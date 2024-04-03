using Godot;
using System.Collections.Generic;


public partial class Main : Node
{
	// Called when the node enters the scene tree for the first time.
	private PackedScene MobsScene;
	private readonly List<Mobs> leftMobsArray = new();
	private readonly List<Mobs> rightMobsArray = new();
	private int _mobsLimit = 20;
	private float _mobsSpeed = 1000.0f;
	public override void _Ready()
	{
		MobsScene = (PackedScene)ResourceLoader.Load("res://Mobs.tscn");
		for(var i = 0; i < _mobsLimit ; i++){
			Vector2 startingPosition = new (i * GD.Randf() * -200.0f,i * GD.Randf() * 100.0f);
			LoadMobs(startingPosition,leftMobsArray);
			if(i > 10) {
				Vector2 oppositePosition = new (1000.0f,i * 50.0f);
				LoadMobs(oppositePosition,rightMobsArray);
			};
		}

	}
	private Mobs LoadMobs(Vector2 startingPosition,List<Mobs> container){
		Mobs Mob = MobsScene.Instantiate<Mobs>();
		Mob.SetPosition(startingPosition);
		AddChild(Mob);
		container.Add(Mob);
		return Mob;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		foreach (var mob in leftMobsArray)
		{
			mob.Move(new Vector2(_mobsSpeed,0));
		}

		foreach (var mob in rightMobsArray)
		{
				mob.Move(new Vector2(-1000.0f,0));
		};

	}


}
