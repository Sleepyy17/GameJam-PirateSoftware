using Godot;
using System;

public partial class OutOfBounds : Node2D
{

     public void _on_area_2d_body_entered(PhysicsBody2D body) {
        if(body.Name == "Knife") {
             GetTree().ReloadCurrentScene();
        }
        
    }
}
