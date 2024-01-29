using Godot;
using System;

public partial class Pause : Control
{
	// Called when the node enters the scene tree for the first time.
	bool pauseOpen = false;

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
	public void OnReplayPressed() {
        GetTree().ReloadCurrentScene();
    }
	public void OnLevelSelectPressed() {
        GetTree().ChangeSceneToFile("res://Components/LevelSelect.tscn");
    }
	public void OnMainMenuPressed() {
        GetTree().ChangeSceneToFile("res://Components/menu.tscn");
    }
}
