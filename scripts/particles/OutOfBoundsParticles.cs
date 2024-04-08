using Godot;
using System;

public partial class OutOfBoundsParticles : CpuParticles2D
{
	// Called when the node enters the scene tree for the first time.
	public bool isOutOfBounds = false;

	public void ApplyParticles(Player player){
		GD.Print("Particles added");
	}
}
