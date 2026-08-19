using Godot;
using System;
using System.ComponentModel;

public partial class GameOver : Control
{
	// Called when the node enters the scene tree for the first time.
	[Export] PackedScene levelScene;

	[Export] Label scoreLabel;
	public override void _Ready()
	{
		scoreLabel.Text = "Your Score " + Global.Score.ToString();
		//levelScene = ResourceLoader.Load<PackedScene>("res://scenes/level.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("shoot"))
		{
			GetTree().ChangeSceneToPacked(levelScene);
		}
	}

    public override void _Input(InputEvent @event)
    {
        if(@event.IsActionPressed("shoot"))
		{
			GetTree().ChangeSceneToPacked(levelScene);
		}
    }


}
