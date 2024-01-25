using Godot;
using System;

public partial class mainPlayer : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 400;

	private float maxHealth = 100;
	private float currentHealth;

	public override void _Ready()
	{
		currentHealth = maxHealth;
	}

	public void GetMovementInput() {
		Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
		Velocity = inputDirection * Speed;
	}

	public override void _PhysicsProcess(double delta) {
		GetMovementInput();
		MoveAndSlide();
		Attack();
	}
	
	public void Attack() {
		
		if (Input.IsActionJustPressed("click")) {
			GetNode<AnimationPlayer>("AnimationPlayer").Play("attack");
			GD.Print(currentHealth);
		}
		
	}
	
	public void takeDamage(float damageTaken)
	{
		currentHealth = currentHealth - damageTaken;
	}
}
