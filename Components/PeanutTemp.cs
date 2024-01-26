using Godot;
using System;

public partial class PeanutTemp : Node2D
{
     private Texture2D knifeWithPeanut;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		knifeWithPeanut = (Texture2D)ResourceLoader.Load("res://Assets/knifeButteredUp.png");
	}

	private void _on_area_2d_body_entered(CharacterBody2D body)
	{	
		Sprite2D knifeSprite = body.GetNode("KnifeSprite") as Sprite2D;
		knifeSprite.Texture = knifeWithPeanut;
	}
}
