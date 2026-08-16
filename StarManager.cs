using Godot;
using System;
using System.Collections.Generic;

public partial class StarManager : Node2D
{
	[Export] PackedScene starScene;
	[Export] int numberOfStars = 100;
	RandomNumberGenerator randomNumberGenerator;
	Vector2 windowSize;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		randomNumberGenerator = new RandomNumberGenerator();
		windowSize = GetViewport().GetVisibleRect().Size;

		for (int i = 0; i < numberOfStars; i++)
		{
			CreateStar();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void CreateStar()
	{
		AnimatedSprite2D star = starScene.Instantiate<AnimatedSprite2D>();
		AddChild(star);

		// Random Position
		int randX = randomNumberGenerator.RandiRange(0, (int)windowSize.X);
		int randY = randomNumberGenerator.RandiRange(0, (int)windowSize.Y);
		star.Position = new Vector2 (randX,randY);

		// Random Size
		float randomScale = (float)randomNumberGenerator.RandiRange(30,60) / 100.0f;
		star.Scale = new Vector2 (randomScale,randomScale);
		
		// Random Animation Speed
		float randAnimationSpeed = (float)randomNumberGenerator.RandiRange (75,300) / 100.0f;
		star.SpeedScale = randAnimationSpeed;
		
		// Rand Animation Start Time
		int startFrame = randomNumberGenerator.RandiRange(1,star.SpriteFrames.GetFrameCount("default"));
		star.Frame = startFrame;

		// Rand Rotation
		int randTilt = randomNumberGenerator.RandiRange (-45,45);
		star.RotationDegrees = randTilt;


	}
}
