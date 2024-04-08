using Godot;
using System;

public partial class OutOfBoundsParticles : CpuParticles2D
{
	public void ApplyParticles <T>(T entity) where T : RigidBody2D{
		float viewportWidth = GetViewport().GetVisibleRect().Size.X;
		float viewportHeight = GetViewport().GetVisibleRect().Size.Y;
		Vector2 entityGlobalPosition = entity.GlobalPosition;
		Vector2 particlesDirection = new();
		float newX = ChangeSign(entityGlobalPosition.X);
		float newY = ChangeSign(entityGlobalPosition.Y);
		GD.Print("X:" + newX + ", Y:" + newY);
		if(entityGlobalPosition.X > viewportWidth || entityGlobalPosition.X < 0){
			particlesDirection = new Vector2(newX,0);
		}else if (entityGlobalPosition.Y > viewportHeight || entityGlobalPosition.Y < 0 ){
			particlesDirection = new Vector2(0,newY);
		}
		Direction = particlesDirection;
		Emitting = true;

	}
	private static float ChangeSign(float value){
		float newValue = MathF.Sign(value);
        return newValue switch
        {
            -1 => 1,
			1 => -1,
            _ => value,
        };
    }

}
