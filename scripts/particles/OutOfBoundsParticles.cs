using Godot;
using System;

public partial class OutOfBoundsParticles : CpuParticles2D
{

	public void ApplyParticles(Node2D entity){
		Emitting = true;
		Direction = entity.GlobalPosition;
	}

}
