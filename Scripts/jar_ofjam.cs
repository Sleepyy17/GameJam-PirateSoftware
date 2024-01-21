using Godot;
using System;

public partial class jar_ofjam : Node2D
{
	private Texture2D knifeWithJam;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		knifeWithJam = (Texture2D)ResourceLoader.Load("res://knifeWithJam.png");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_area_2d_body_entered(CharacterBody2D body)
	{	
		
		Sprite2D knifeSprite = body.GetNode("KnifeSprite") as Sprite2D;
		knifeSprite.Texture = knifeWithJam;
	}
	

}



