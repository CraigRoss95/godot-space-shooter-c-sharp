using Godot;
using System;
using System.ComponentModel;

public partial class Laser : Area2D
{
	[Export] int speed = 500;

	[Export] Sprite2D laserSprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		laserSprite.Scale = new Vector2 (0,0);
		Tween elongateLaserTween = CreateTween();
		elongateLaserTween.TweenProperty(laserSprite, "scale", new Vector2 (1,1), 0.2);
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position = new Vector2(Position.X, Position.Y - speed * (float)delta);
	}
}
