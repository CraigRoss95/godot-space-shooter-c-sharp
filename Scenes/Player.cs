using Godot;
using System;
using System.Dynamic;

public partial class Player : CharacterBody2D
{

	[Export]
	int speed = 500; 

	public override void _Ready()
	{
		Position = new Vector2(100, 500);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		Vector2 direction = Input.GetVector("moveLeft", "moveRight", "moveUp", "moveDown");

		Velocity = direction * speed;
		this.MoveAndSlide();

		if(Input.IsActionJustPressed("shoot"))
		{
			//Emit brodcast from EventManager
			EventManager.BrodcastFireLaserEvent(Position);
		}
	}
}


