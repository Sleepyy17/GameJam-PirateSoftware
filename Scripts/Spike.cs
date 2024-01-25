using Godot;
using System;

public partial class Spike : Node2D
{
	private mainPlayer mainCharacter;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
	
	private void _on_area_2d_body_entered(PhysicsBody2D body) {
		GD.Print("Player Stepped On a Spike!");
		if (body.Name == "mainPlayer") {
			mainPlayer mainCharacter = body as mainPlayer;
			if (mainCharacter != null) {
				mainCharacter.takeDamage(15);
			}
		}
	}
}





