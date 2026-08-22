using Godot;
using System;
using System.Dynamic;

public partial class Player : CharacterBody2D
{

	[Export] int speed = 500;
	[Export] AudioStreamPlayer laserSound;
	private AudioStreamPlayer damageSound;
	[Export] Marker2D laserStartPos;
	[Export] Timer laserCooldownTimer;

	private bool offCooldown = true;
	public override void _Ready()
	{
		//Setup
		Position = new Vector2(100, 500);
		damageSound = GetNode<AudioStreamPlayer>("/root/DamageSound");

		//laserCooldownTimer.Timeout += SetOffCooldownTrue;
		laserCooldownTimer.Connect("timeout", Callable.From(SetOffCooldownTrue));

		EventManager.MeteorImpactEvent += MakeDamageSound;
		//Listeners

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		HandelInput();
		HandelShooting();
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		//laserCooldownTimer.Timeout -= SetOffCooldownTrue;
		EventManager.MeteorImpactEvent -= MakeDamageSound;

	}


	void SetOffCooldownTrue() { offCooldown = true; }

	void MakeDamageSound() { damageSound.Play(); }

	void HandelInput()
	{
		Vector2 direction = Input.GetVector("moveLeft", "moveRight", "moveUp", "moveDown");

		Velocity = direction * speed;
		MoveAndSlide();
	}

	void HandelShooting()
	{
		if (Input.IsActionPressed("shoot") && offCooldown == true)
		{
			laserCooldownTimer.Start();
			offCooldown = false;
			//Emit brodcast from EventManager
			laserSound.Play();
			EventManager.BrodcastFireLaserEvent(laserStartPos.GlobalPosition);
		}
	}
}


