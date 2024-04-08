using System.Xml;
using Godot;

public partial class Player : RigidBody2D 
{
	private float _force = 1000.0f;
	float _gravity = 1.0f;
	private float _accelerationX = 1000.0f;
	private float  _jumpAcceleration = 20000.0f;
	public bool isOutOfBounds = false;
	private OutOfBoundsParticles outOfBoundsParticles;


    public override void _Ready()
    {
		outOfBoundsParticles = GetNode<OutOfBoundsParticles>("OutOfBoundsParticles");
		GD.Print(outOfBoundsParticles);
    }

    private static Vector2 CalculateNewForce(float x, float y ){
		Vector2 force = new(x,y);
		return force;
	}
	private void HandleInputs()
	{
		if(Input.IsActionPressed("left")){
			ApplyForce(CalculateNewForce(-Mass * _accelerationX,0));
		}

		if(Input.IsActionPressed("right")){
			ApplyForce(CalculateNewForce(Mass * _accelerationX,0));
		}
	}

    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
    	if(Input.IsActionJustPressed("jump")){
			var startingVelocity = state.LinearVelocity.Y;
			Vector2 force = new( 0,-Mass * _jumpAcceleration); // to initiate jump
			float endVelocity = startingVelocity + _jumpAcceleration * (float)GetProcessDeltaTime();
			var currentAcceleration = Mathf.Abs(endVelocity-startingVelocity)/(float)GetProcessDeltaTime();
			if(currentAcceleration < _jumpAcceleration){
				ApplyForce(force);
				return;
			}
			var deceleration = (endVelocity - startingVelocity) / (float)GetProcessDeltaTime() ;
			Vector2 newCalculatedForce = new(0, Mass * -deceleration);
			ApplyForce(newCalculatedForce);
    	}
    }
    public override void _Process(double delta)
	{
		HandleInputs();
		CheckPlayerOutOfBounds();
	}

	private void CheckPlayerOutOfBounds(){
		float viewportWidth = GetViewport().GetVisibleRect().Size.X;
		float viewportHeight = GetViewport().GetVisibleRect().Size.Y;
		outOfBoundsParticles.ApplyParticles(this);
        if(GlobalPosition.X < 0){
			GD.Print("left");
        } else if(GlobalPosition.X > viewportWidth ){
			GD.Print("right");
        } else if(GlobalPosition.Y < 0 ){
			GD.Print("top");
    	} else if(GlobalPosition.Y > viewportHeight ){
			GD.Print("bottom");
    	}
	}
}
