using Godot;
using System;

public partial class menu : Control
{
    public void onStartPressed() {
        GetTree().ChangeSceneToFile("res://Components/LevelSelect.tscn");
    }
    public void onOptionsPressed() {
        
    }
    public void onQuitPressed() {
        GetTree().Quit();
    }
}
