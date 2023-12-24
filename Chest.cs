using Godot;
using System;

public partial class Chest : Node2D
{
	[Export]
	private bool isOpen = false;
	private bool isPlayerInRange = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		if (Input.IsActionJustPressed("interact") && isPlayerInRange) {
			InteractChest();
		}
	}

	private void InteractChest() {
		if (isOpen) {
			GetNode<AnimationPlayer>("AnimationPlayer").Play("chest_close");
			isOpen = false;
		}
		else {
			GetNode<AnimationPlayer>("AnimationPlayer").Play("chest_open");
			isOpen = true;
		}
	}
	private void _on_interaction_area_body_entered(PhysicsBody2D body) {
        // Check if the area is the player
			GD.Print("Player entered");
        if (body.Name == "mainPlayer") {
            isPlayerInRange = true;
        }
    }

    private void _on_interaction_area_body_exited(PhysicsBody2D body) {
        // Check if the area is the player
			GD.Print("Player exited");
        if (body.Name == "mainPlayer") {
            isPlayerInRange = false;
        }
    }
}
