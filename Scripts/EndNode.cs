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
    //    PlayerCamera2D = GetNode<Camera2D>("../Knife/Camera2D");
       WinScreen = GetNode<Control>("../Node2D/WinScreen");
       GD.Print(PlayerCamera2D);
       GD.Print(WinScreen);
       GD.Print(tree);
    }

    public void _on_area_2d_area_entered(Area2D area) {
        if (area.Name == "BladeArea") {
            GD.Print(name);
            if (name == "LevelOne") {
                g.LevelOneComplete = true;
            } else if (name == "LevelTwo") {
                g.LevelTwoComplete = true;
            }
            GD.Print("Yefewfeway");
            GD.Print(WinScreen.Visible);
            WinScreen.Visible = true;
            tree.Paused = true;
        }
    }
}
