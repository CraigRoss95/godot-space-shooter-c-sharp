using Godot;
using System;
using System.Runtime.InteropServices;

public partial class Level : Node2D
{
	PackedScene meteorScene = ResourceLoader.Load<PackedScene>("res://scenes/meteor.tscn");
	Node meteorParent = new Node();

	Timer timer;
	

	public override void _Ready()
	{
		meteorParent = GetNode<Node>("MeteorParent");
		timer = GetNode<Timer>("MeteorTimer");

		//Link events on creation to functions
		timer.Timeout += CreateMeteor;
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
		Node meteor = meteorScene.Instantiate();
		meteorParent.AddChild(meteor);
		return;
	}

	public void ShootLaser(Vector2 position)
	{
		GD.Print("kerblamo! at " + position);
		
	}
}
