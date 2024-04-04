using Godot;
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
		float min = viewportWidth;
		float max = viewportWidth+300;
		for(var i = 0; i < _mobsLimit ; i++){
			if(i > 10) {
				// random from viewportwidth to 200;
				Vector2 oppositePosition = new (GD.Randf() *(min-max) + viewportWidth,i * 50.0f);
				LoadMobs(oppositePosition);
				continue;	
			};
			Vector2 startingPosition = new (i * GD.Randf() * -200.0f,i * GD.Randf() * 100.0f);
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
				continue;
			}
			mob.Move(new Vector2(_mobsSpeed,0));
		}
	}
	private void ResetMobPosition(){
		float viewportWidth = GetViewport().GetVisibleRect().Size.X;
		for(var i = 0; i < mobsArray.Count; i++){
			Mobs mob = mobsArray[i];
			if(i > mobsArray.Count/2){
				if(mob.Position.X < -100.0f){
					mob.SetPosition(new Vector2(i * GD.Randf() * -200.0f,i * GD.Randf() * 100.0f));
				}
				continue;
			}
			if(mob.Position.X > viewportWidth){
				Vector2 startingPosition = new (i * GD.Randf() * -200.0f,i * GD.Randf() * 100.0f);
				mob.SetPosition(startingPosition);
			}
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		MoveMobs();
		ResetMobPosition();
	}

	
}
