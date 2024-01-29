using Godot;
using System;

public partial class levelFour : Node2D
{
    CharacterBody2D player;
	public override void _Ready()
	{
		player = GetNode<CharacterBody2D>("Knife");
	}
	public void _on_area_2d_body_entered(PhysicsBody2D body) {
		if(body.Name == "Knife") {
			body.GlobalPosition = new Vector2(-1480, 571);
			player.Velocity = new Vector2(0, 0);
		}
	}
}
