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
    public override void _Ready()
    {
        SceneTree tree = GetTree();
       if (tree.Paused == true) {
        tree.Paused = false;
       }
    }
    
}
