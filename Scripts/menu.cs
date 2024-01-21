using Godot;
using System;

public partial class menu : Control
{
    public void onStartPressed() {
        GetTree().ChangeSceneToFile("res://Components/level_idea.tscn");
    }
    public void onOptionsPressed() {
        
    }
    public void onQuitPressed() {
        GetTree().Quit();
    }
}
