using System;
using Godot;

public partial class Meteor : Area2D
{
	// Called when the node enters the scene tree for the first time.
	int moveSpeed = 0;
	int rotationSpeed = 0;
	float directionX = 0.0f;
	bool canColide = true;

	Sprite2D meteorSprite;

	[Export] AudioStreamPlayer meteorExploadSound;

	public override void _Ready()
	{
		// I tried to make this one line but couldn't
		BodyEntered += OnBodyEntered;
		AreaEntered += OnAreaEntered;
		SetupRandom();
	}

    public override void _ExitTree()
    {
        base._ExitTree();
			BodyEntered -= OnBodyEntered;
			AreaEntered -= OnAreaEntered;

    }


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += new Vector2 (directionX, 1) * moveSpeed * (float)delta;
		RotationDegrees += rotationSpeed * (float)delta;
	}

	
	void OnBodyEntered(Node2D body)
	{
		if (canColide)
		{
			EventManager.BrodcastMeteorImpact();
			
			QueueFree();	
		}
			
	}

	private async void OnAreaEntered(Node2D area)
	{
		if(canColide)
		{
			//TODO Check if it's a laser
			
			area.QueueFree();
			canColide = false;
			meteorExploadSound.Play();
			meteorSprite.Visible = false;
			await ToSignal(GetTree().CreateTimer(1), "timeout");
			QueueFree();
		}
	}

	void SetupRandom()
	{
		RandomNumberGenerator randomNumberGenerator = new RandomNumberGenerator();
		int spriteIndex = randomNumberGenerator.RandiRange(0,3);
		meteorSprite = GetNode<Sprite2D>("MeteorSprite" + spriteIndex);
		meteorSprite.Visible = true;

		moveSpeed = randomNumberGenerator.RandiRange(200,500);
		rotationSpeed = randomNumberGenerator.RandiRange(-40,40);
		directionX = randomNumberGenerator.RandfRange(-1,1);


		int width = (int)GetViewport().GetVisibleRect().Size.X;
		 
		float randW = randomNumberGenerator.RandiRange(0, width);
		float randH = randomNumberGenerator.RandiRange(-50, -150);

		Position = new Vector2(randW,randH);
	}
	

	
}
