using Godot;
using System;

public partial class win_screen : Control
{
    public string name;
    public SceneTree tree;
    public override void _Ready()
    {
        tree = GetTree();
        name = tree.CurrentScene.Name;
        if (name == "LevelOne") {
            name = "LevelTwo";
        } else if (name == "LevelTwo") {
            name = "LevelThree";
        }
    }

    public void OnNextLevelPressed() {
        tree.ChangeSceneToFile("res://Components/" + name + ".tscn");
    }
    public void OnMenuPressed() {
        tree.ChangeSceneToFile("res://Components/menu.tscn");
    }
    
}
