using Godot;
using System;
using System.Collections.Generic;


public partial class Main : Node
{
	// Called when the node enters the scene tree for the first time.
	private PackedScene MobsScene;
	private readonly List<Mobs> mobsArray = new();
	private int _mobsLimit = 20;
	private float _mobsSpeed = 1000.0f;
	public override void _Ready()
	{
		GD.Print(GetViewport().GetVisibleRect().Size.X);
		MobsScene = (PackedScene)ResourceLoader.Load("res://Mobs.tscn");
		float viewportWidth = GetViewport().GetVisibleRect().Size.X;
		float viewportHeight = GetViewport().GetVisibleRect().Size.Y;
		float min = viewportWidth;
		float max = viewportWidth+300;
		for(var i = 0; i < _mobsLimit ; i++){
			if(i > 10) {
				Vector2 oppositePosition = new (viewportWidth - 300, GD.Randf() * viewportHeight - 300);
				// random from viewportwidth to 200;
				LoadMobs(oppositePosition);
				continue;	
			};
			Vector2 startingPosition = new (200, GD.Randf() * viewportHeight - 300);
			LoadMobs(startingPosition);
		}
	}
	private Mobs LoadMobs(Vector2 startingPosition){
		Mobs Mob = MobsScene.Instantiate<Mobs>();
		Mob.SetPosition(startingPosition);
		AddChild(Mob);
		mobsArray.Add(Mob);
		return Mob;
	}
	private void MoveMobs(){
		for(var i = 0; i < mobsArray.Count ; i++){
			Mobs mob = mobsArray[i];
			if(i > mobsArray.Count/2){
				mob.Move(new Vector2(-_mobsSpeed,0));
				CheckMobPositions(mob);
				continue;
			}
			mob.Move(new Vector2(_mobsSpeed,0));
			CheckMobPositions(mob);
		}
	}

	private void CheckMobPositions(Mobs mob){
		float viewportWidth = GetViewport().GetVisibleRect().Size.X;
		float viewportHeight = GetViewport().GetVisibleRect().Size.Y;
		float min = viewportWidth;
		float max = viewportWidth+300;
		// Vector2 oppositePosition = new (GD.Randf() *(min-max) + viewportWidth, GD.Randf() * viewportHeight);
		// Vector2 startingPosition = new (GD.Randf() * -200.0f, GD.Randf() * viewportHeight);
		Vector2 oppositePosition = new (viewportWidth - 300, GD.Randf() * viewportHeight - 300);
		Vector2 startingPosition = new (200, GD.Randf() * viewportHeight - 300);

		if(mob.Position.X < 0){
			ResetMobPosition(mob,oppositePosition);
			return;
		};
		if(mob.Position.X > viewportWidth ){
			ResetMobPosition(mob,startingPosition);
			return;
		};
	}

	public void ResetMobPosition(Mobs mob, Vector2 newDirection ){
		Mobs outBoundsMob = mobsArray.Find(subMob=> subMob.GetIndex() == mob.GetIndex());
		outBoundsMob.Position = newDirection;
	}

	public override void _Process(double delta)
	{
		MoveMobs();
	}

}
