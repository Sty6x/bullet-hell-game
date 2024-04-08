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
		GD.Print(collisionRect);
		DrawCircle(position,collisionRect.Size.X,new Color("#0091AD"));
	}
}
