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
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		MoveMobs();
	}

	private void MoveMobs(){
		var first = 0;
		var end = mobsArray.Count -1;
		var middle = (first + end) / 2;
		for(var i = 0; i < end; i++){
			if(i > middle){
				mobsArray[i].Move(new Vector2(-_mobsSpeed,0));
				continue;
			}
			mobsArray[i].Move(new Vector2(_mobsSpeed,0));
		}
	}
}
