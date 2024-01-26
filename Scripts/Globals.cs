using Godot;
using System;

public partial class Globals : Node
{
    // TrueFalse values for level select scene, when a level is finished end node should update these values
    public bool LevelOneComplete = false;
    public bool LevelTwoComplete = false;
    public bool LevelThreeComplete = false;
    public bool LevelFourComplete = false;


    // Integers to keep track of the total clicks/deaths of a playthrough 
    public int totalGameClicks = 0;
    public int totalGameDeaths = 0;

    public int totalLevelOneClicks = 0;
    public int totalLevelOneDeaths = 0;

    public int totalLevelTwoClicks = 0;
    public int totalLevelTwoDeaths = 0;

    public int totalLevelThreeClicks = 0;
    public int totalLevelThreeDeaths = 0;

    public int totalLevelFourClicks = 0;
    public int totalLevelFourDeaths = 0;

    public int clickCounter = 0;

    public AudioStreamPlayer AudioPlayer;
    public override void _Ready()
    {
        AudioPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
    }
    public override void _Process(double delta) {
        if (AudioPlayer.Playing == false) {
            AudioPlayer.Play();
        }
    }
}
