using Godot;
using System;

public partial class EndNode : Node2D
{
    public void _on_area_2d_area_entered(Area2D area) {
        if (area.Name == "BladeArea") {
            GD.Print("Yay");
        }
    }
}
