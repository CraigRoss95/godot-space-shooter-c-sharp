using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public partial class Ui : CanvasLayer
{
	[Export] PackedScene lifeIconScene;
	[Export] HBoxContainer lifeRowHBox;
	[Export] Timer timer;
	[Export] Label scoreLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Global.Score = 0;
		timer.Timeout += ScorePoint;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}


    public override void _ExitTree()
    {
        base._ExitTree();
		timer.Timeout -= ScorePoint;
    }


	public void ScorePoint()
	{
		Global.Score += 1;
		scoreLabel.Text = Global.Score.ToString();
	}
	public void SetHealthUi(int playerLives)
	{
		if (playerLives >= 0)
		{
			
			//Destory icons
			while (lifeRowHBox.GetChildCount() > 0)
			{
				lifeRowHBox.GetChild(0).Free();
			}

			//Re-add icons
			for (int i = 0; i < playerLives; i++)
			{
				TextureRect lifeIcon = lifeIconScene.Instantiate<TextureRect>();
				lifeRowHBox.AddChild(lifeIcon);
			}

		}
		
	}
}
