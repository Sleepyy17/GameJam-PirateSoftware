using Godot;
using System;

public partial class level_idea_2 : Node2D
{

	CharacterBody2D player;
	public override void _Ready()
	{
		player = GetNode<CharacterBody2D>("Knife");
	}

	public void _on_area_2d_body_entered(PhysicsBody2D body) {
		if(body.Name == "Knife") {
			body.GlobalPosition = new Vector2(-511, -111);
			player.Velocity = new Vector2(0,0);
		}
		
	}


}
