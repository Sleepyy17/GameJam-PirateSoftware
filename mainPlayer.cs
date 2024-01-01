using Godot;
using System;

public partial class mainPlayer : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 400;
	private AnimatedSprite2D animatedSprite; 

	public override void _Ready()
	{
	}

	public void GetMovementInput() {
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
		Velocity = inputDirection * Speed;
	}

	public override void _PhysicsProcess(double delta) {
		GetMovementInput();
		MoveAndSlide();
		Attack();
	}
	
	public void Attack() {
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		if (Input.IsActionJustPressed("click")) {
			animatedSprite.Play("attack");
		}
	}
}
