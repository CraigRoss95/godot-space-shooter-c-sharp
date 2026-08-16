using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Ui : CanvasLayer
{
	[Export] PackedScene lifeIconScene;
	[Export] HBoxContainer lifeRowHBox;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
