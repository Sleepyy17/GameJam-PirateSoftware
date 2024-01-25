using Godot;
using System;

public partial class Pause : Control
{
	// Called when the node enters the scene tree for the first time.
	bool pauseOpen = false;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("pause") && !pauseOpen) {
			GetTree().Paused = true;
			Show();
			pauseOpen = true;
		} 
		else if (Input.IsActionJustPressed("pause") && pauseOpen) {
			GetTree().Paused = false;
			Hide();
			pauseOpen = false;
		}
	}
}
