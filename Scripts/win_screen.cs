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
    }

    public void OnNextLevelPressed() {
        if (name == "LevelOne") {
            tree.ChangeSceneToFile("res://Components/LevelTwo.tscn");
        } else if (name == "LevelTwo") {
            tree.ChangeSceneToFile("res://Components/LevelThree.tscn");
        } else if (name == "LevelThree") {
            tree.ChangeSceneToFile("res://Components/LevelFour.tscn");
        } 
        
    }
    public void OnMenuPressed() {
        tree.ChangeSceneToFile("res://Components/menu.tscn");
    }
    
}
