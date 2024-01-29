using Godot;
using System;

public partial class EndNode : Node2D
{
    public Globals g;
    public Camera2D PlayerCamera2D;
    public Control WinScreen;
    public string name;
    public SceneTree tree;
    public override void _Ready()
    {
       g = (Globals)GetNode("/root/Globals");
       tree = GetTree();
       name = tree.CurrentScene.Name;
       if (tree.Paused == true) {
        tree.Paused = false;
       }

       WinScreen = GetNode<Control>("../Node2D/WinScreen");

       GD.Print(WinScreen);
       GD.Print(tree);
    }

    public void _on_area_2d_area_entered(Area2D area) {
        if (area.Name == "BladeArea") {
            
            if (name == "LevelOne") {
                g.LevelOneComplete = true;
            } else if (name == "LevelTwo") {
                g.LevelTwoComplete = true;
            } else if (name == "LevelThree") {
                g.LevelThreeComplete = true;
            } else if (name == "LevelFour") {
                g.LevelFourComplete = true;
            }
            WinScreen.Visible = true;
            tree.Paused = true;
        }
    }
}
