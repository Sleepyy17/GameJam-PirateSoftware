using Godot;
using System;

public partial class ScoreLabel : Label
{
	// Called when the node enters the scene tree for the first time.
	public Globals g;
	public Node scene;

	public override void _Ready()
	{
		scene = GetTree().CurrentScene;
		g = (Globals)GetNode("/root/Globals");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		int totalLevelClicks = -1;
		int totalLevelDeaths = -1;
		if (scene.Name == "LevelOne") {
			totalLevelClicks = g.totalLevelOneClicks;
			totalLevelDeaths = g.totalLevelOneDeaths;
		} else if (scene.Name == "LevelTwo") {
			totalLevelClicks = g.totalLevelTwoClicks;
			totalLevelDeaths = g.totalLevelTwoDeaths;
		} else if (scene.Name == "LevelThree") {
			totalLevelClicks = g.totalLevelThreeClicks;
			totalLevelDeaths = g.totalLevelThreeDeaths;
		} else if (scene.Name == "LevelFour") {
			totalLevelClicks = g.totalLevelFourClicks;
			totalLevelDeaths = g.totalLevelFourDeaths;
		}
		this.Text = "Clicks: " + Convert.ToString(g.clickCounter) + "\n" 
				  + "Deaths: " + totalLevelDeaths + "\n"
				  + "Total Clicks: " + totalLevelClicks + "\n" ;
		//GD.Print($"BladeContactState: {bladeContactState}, BladeSpreadState: {bladeSpreadState}, KnifeAreaContact: {knifeAreaContact}");

	}
}
