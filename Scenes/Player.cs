using Godot;
using System;
using System.Dynamic;

public partial class Player : CharacterBody2D
{

	[Export]
	int speed = 500; 

	[Export]
	AudioStreamPlayer laserSound;
	[Export]
	AudioStreamPlayer damageSound;
	Marker2D laserStartPos = new Marker2D();
	Timer laserCooldownTimer = new Timer();

	private bool offCooldown = true;
	public override void _Ready()
	{
		//Setup
		laserStartPos = GetNode<Marker2D>("LaserStartPos");
		laserCooldownTimer = GetNode<Timer>("LaserCooldownTimer");
		Position = new Vector2(100, 500);

		laserCooldownTimer.Timeout += SetOffCooldownTrue;
		EventManager.MeteorImpactEvent += MakeDamageSound;
		//Listeners

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		Vector2 direction = Input.GetVector("moveLeft", "moveRight", "moveUp", "moveDown");

		Velocity = direction * speed;
		MoveAndSlide();

		if(Input.IsActionPressed("shoot") && offCooldown == true)
		{
			laserCooldownTimer.Start();
			offCooldown = false;
			//Emit brodcast from EventManager
			laserSound.Play();
			EventManager.BrodcastFireLaserEvent(laserStartPos.GlobalPosition);
		}
	}

    public override void _ExitTree()
    {
        base._ExitTree();
		laserCooldownTimer.Timeout -= SetOffCooldownTrue;
		EventManager.MeteorImpactEvent -= MakeDamageSound;

    }


	void SetOffCooldownTrue()
	{
		offCooldown = true;	
	}

	void MakeDamageSound()
	{
		damageSound.Play();
	}
}


