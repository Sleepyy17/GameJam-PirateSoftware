using Godot;
using System;

public partial class OutOfBounds : Node2D
{
     public Globals g;
     public Node scene;

     public override void _Ready()
     {
          scene = GetTree().CurrentScene;
          g = (Globals)GetNode("/root/Globals");
     }

     public void _on_area_2d_body_entered(PhysicsBody2D body) {
          if (body.Name == "Knife") {
               if (scene.Name == "LevelOne") {
                    g.totalLevelOneDeaths++;
               } else if (scene.Name == "LevelTwo") {
                    g.totalLevelTwoDeaths++;
               } else if (scene.Name == "LevelThree") {
                    g.totalLevelThreeDeaths++;
               }
               g.totalGameDeaths++;
          GetTree().ReloadCurrentScene();
        }
        
    }
}
