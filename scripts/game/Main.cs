using Godot;
using System;
using System.Collections.Generic;


public partial class Main : Node
{
	// Called when the node enters the scene tree for the first time.
	private PackedScene MobsScene;
	private readonly List<Mobs> mobsArray = new();
	private int _mobsLimit = 10;
	private float _mobsSpeed = 1000.0f;
	private Player player;

	public override void _Ready()
	{
		player = GetNode<Player>("./Player");
		MobsScene = (PackedScene)ResourceLoader.Load("res://Mobs.tscn");
		// CreateMobs();
	}
	private void CreateMobs(){
		float viewportWidth = GetViewport().GetVisibleRect().Size.X;
		float viewportHeight = GetViewport().GetVisibleRect().Size.Y;
		float min = viewportWidth;
		float max = viewportWidth+300;
		for(var i = 0; i < _mobsLimit ; i++){
			if(i > 10) {
				Vector2 oppositePosition = new (viewportWidth - 300, GD.Randf() * viewportHeight - 300);
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
			CheckMobPositions(mob);
			if(i > mobsArray.Count/2){
				mob.Move(new Vector2(-_mobsSpeed,0));
				continue;
			}  
			mob.Move(new Vector2(_mobsSpeed,0));
		}
	}

	private void CheckMobPositions(Mobs mob){
		float viewportWidth = GetViewport().GetVisibleRect().Size.X;
		float viewportHeight = GetViewport().GetVisibleRect().Size.Y;
		float min = viewportWidth;
		float max = viewportWidth+300;
		Vector2 oppositePosition = new (viewportWidth - 300, GD.Randf() * viewportHeight - 300);
		Vector2 startingPosition = new (200, GD.Randf() * viewportHeight - 300);

		if(mob.GlobalPosition.X < 0){
			ResetMobPosition(mob, oppositePosition);
		} else if(mob.GlobalPosition.X > viewportWidth ){
			ResetMobPosition(mob, startingPosition);
		}
	}

	public void ResetMobPosition(Mobs mob, Vector2 newPosition ){
		Mobs outBoundsMob = mobsArray.Find(subMob=> subMob.GetIndex() == mob.GetIndex());
		outBoundsMob.SetPosition(newPosition);
	}

	public override void _Process(double delta)
	{
		MoveMobs();
		CheckPlayerOutOfBounds();
	}

	private void CheckPlayerOutOfBounds(){
		float viewportWidth = GetViewport().GetVisibleRect().Size.X;
		float viewportHeight = GetViewport().GetVisibleRect().Size.Y;
        if(player.GlobalPosition.X < 0){
			GD.Print("left");
        } else if(player.GlobalPosition.X > viewportWidth ){
			GD.Print("right");
        } else if(player.GlobalPosition.Y < 0 ){
			GD.Print("top");
    	} else if(player.GlobalPosition.Y > viewportHeight ){
			GD.Print("bottom");
    	}
	}

}
