using Godot;
using System;
using System.Runtime.InteropServices;

public partial class Level : Node2D
{
	PackedScene meteorScene = ResourceLoader.Load<PackedScene>("res://scenes/meteor.tscn");
	PackedScene laserScene = ResourceLoader.Load<PackedScene>("res://scenes/laser.tscn");
	Node2D meteorParent = new Node2D();
	Node2D laserParent = new Node2D();

	Timer timer;

	int playerLives = 3;
	

	public override void _Ready()
	{
		meteorParent = GetNode<Node2D>("MeteorParent");
		laserParent = GetNode<Node2D>("LaserParent");
		timer = GetNode<Timer>("MeteorTimer");

		//Link events on creation to functions
		timer.Timeout += CreateMeteor;
		EventManager.MeteorImpactEvent += MeteorImpactFunc;
		EventManager.FireLaserEvent += ShootLaser;	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _ExitTree()
    {
		//Destroy event on leaving
		timer.Timeout -= CreateMeteor;
		EventManager.FireLaserEvent -= ShootLaser;	
        base._ExitTree();
    }


	void CreateMeteor ()
	{
		Node2D meteor = meteorScene.Instantiate<Node2D>();
		meteorParent.AddChild(meteor);
		return;
	}

	public void ShootLaser(Vector2 position)
	{
		Node2D laser = laserScene.Instantiate<Node2D>();
		laser.Position = position;
				laserParent.AddChild(laser);
		
	}

	void MeteorImpactFunc()
	{
		playerLives -= 1;
		if (playerLives <= 0)
		{
			GD.Print ("You Died");
		}
	}
}
