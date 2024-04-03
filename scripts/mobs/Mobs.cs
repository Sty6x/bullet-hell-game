using Godot;
using System;

public partial class Mobs : RigidBody2D
{
	public void SetPosition(Vector2 position){
		Position = position;
	}
	public void Move(Vector2 direction){
		ApplyForce(direction);
	}
}
