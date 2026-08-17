using System;
using Godot;

public partial class Meteor : Area2D
{
	// Called when the node enters the scene tree for the first time.
	int moveSpeed = 0;
	int rotationSpeed = 0;
	float directionX = 0.0f;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;

		RandomNumberGenerator randomNumberGenerator = new RandomNumberGenerator();
		int spriteIndex = randomNumberGenerator.RandiRange(0,3);
		GetNode<Sprite2D>("MeteorSprite" + spriteIndex).Visible = true;

		moveSpeed = randomNumberGenerator.RandiRange(200,500);
		rotationSpeed = randomNumberGenerator.RandiRange(-40,40);
		directionX = randomNumberGenerator.RandfRange(-1,1);


		int width = (int)GetViewport().GetVisibleRect().Size.X;
		 
		float randW = randomNumberGenerator.RandiRange(0, width);
		float randH = randomNumberGenerator.RandiRange(-50, -150);

		Position = new Vector2(randW,randH);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += new Vector2 (directionX, 1) * moveSpeed * (float)delta;
		RotationDegrees += rotationSpeed * (float)delta;
	}

	
	private void OnBodyEntered(Node2D body)
	{
			EventManager.BrodcastMeteorImpact();
			
			QueueFree();
			
	}

	

	
}
