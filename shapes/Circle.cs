using Godot;
using System;

public partial class Circle : Node2D
{
    // Called when the node enters the scene tree for the first time.


    public override void _Draw()
    {
		CollisionShape2D collisionShape = GetNode<CollisionShape2D>("../CollisionShape2D");
		GD.Print(collisionShape.Shape.GetRect());
		var position = new Vector2(0,0);
		var collisionRect = collisionShape.Shape.GetRect();
		DrawCircle(position,28,new Color("#0091AD"));
	}
    public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
