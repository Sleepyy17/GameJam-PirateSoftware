using Godot;
using System;

public partial class LevelSelect : Control
{
    public MarginContainer LockOne;
    public MarginContainer LockTwo;
    public MarginContainer LockThree;
     public MarginContainer LockFour;
    public Globals g;

    public override void _Ready()
    {
        g = (Globals)GetNode("/root/Globals");
        LockOne = GetNode<MarginContainer>("GridContainer/PanelContainer/Lock1");
        LockTwo = GetNode<MarginContainer>("GridContainer/PanelContainer2/Lock2");
        LockThree = GetNode<MarginContainer>("GridContainer/PanelContainer3/Lock3");
        LockFour = GetNode<MarginContainer>("GridContainer/PanelContainer4/Lock4");
        LockOne.QueueFree();
        if (g.LevelOneComplete) {
            LockTwo.QueueFree();
            if (g.LevelTwoComplete) {
                LockThree.QueueFree();
                if (g.LevelThreeComplete) {
                    LockFour.QueueFree();
                }
            }
        }
    SceneTree tree = GetTree();
    if (tree.Paused == true) {
        tree.Paused = false;
    }
    
    }
    public void OnLevelOnePressed() {
        GetTree().ChangeSceneToFile("res://Components/levelOne.tscn");
    }

    public void OnLevelTwoPressed() {
        if (g.LevelOneComplete) {
            GetTree().ChangeSceneToFile("res://Components/levelTwo.tscn");
        }
    }

    public void OnLevelThreePressed() {
        if (g.LevelTwoComplete) {
            GetTree().ChangeSceneToFile("res://Components/levelThree.tscn");
        }
    }
    public void OnLevelFourPressed() {
        if (g.LevelThreeComplete) {
            GetTree().ChangeSceneToFile("res://Components/levelFour.tscn");
        }
    }
    public void OnBackPressed() {
        GetTree().ChangeSceneToFile("res://Components/menu.tscn");
    }
}