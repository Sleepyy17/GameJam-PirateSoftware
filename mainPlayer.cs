using Godot;
using System;

public partial class mainPlayer : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 400;
	private AnimatedSprite2D animatedSprite; 

	public override void _Ready()
	{
		// Get a reference to the AnimatedSprite2D node in your scene
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public void GetInput() {

		Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
		Velocity = inputDirection * Speed;
		if (Input.IsActionJustPressed("click")) {
			animatedSprite.Play("attack");
		} 
	}

	public override void _PhysicsProcess(double delta) {
		GetInput();
		MoveAndSlide();
	}
}
