using Godot;
using System;
[Tool]
public partial class LevelSelect : Control
{
    public MarginContainer LockOne;
    public MarginContainer LockTwo;
    public MarginContainer LockThree;
    public  Globals g;

    public override void _Ready()
    {
       g = (Globals)GetNode("/root/Globals");
       LockOne = GetNode<MarginContainer>("GridContainer/PanelContainer/Lock1");
       LockTwo = GetNode<MarginContainer>("GridContainer/PanelContainer2/Lock2");
       LockThree = GetNode<MarginContainer>("GridContainer/PanelContainer3/Lock3");
       if (g.LevelOne) {
        LockOne.QueueFree();
       }
    }
    public void OnLevelOnePressed() {
        if (g.LevelOne) {
            GetTree().ChangeSceneToFile("res://Components/levelOne.tscn");
        }
    }

    public void OnLevelTwoPressed() {
        if (g.LevelTwo) {
            GetTree().ChangeSceneToFile("res://Components/levelTwo.tscn");
        }
    }

    public void OnLevelThreePressed() {
        if (g.LevelThree) {
            GetTree().ChangeSceneToFile("res://Components/levelThree.tscn");
        }
    }
}