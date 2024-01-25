using Godot;
using System;

public partial class LevelSelect : Control
{
    public MarginContainer LockOne;
    public MarginContainer LockTwo;
    public MarginContainer LockThree;
    public Globals g;

    public override void _Ready()
    {
        g = (Globals)GetNode("/root/Globals");
        LockOne = GetNode<MarginContainer>("GridContainer/PanelContainer/Lock1");
        LockTwo = GetNode<MarginContainer>("GridContainer/PanelContainer2/Lock2");
        LockThree = GetNode<MarginContainer>("GridContainer/PanelContainer3/Lock3");
        LockOne.QueueFree();
<<<<<<< Updated upstream
       }
       if (g.LevelTwo) {
        LockTwo.QueueFree();
       }
=======
        if (g.LevelOneComplete) {
            LockTwo.QueueFree();
            if (g.LevelTwoComplete) {
                LockThree.QueueFree();
                if (g.LevelThreeComplete) {
                    
                }
            }
        }
    SceneTree tree = GetTree();
    if (tree.Paused == true) {
        tree.Paused = false;
    }
    
>>>>>>> Stashed changes
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
}