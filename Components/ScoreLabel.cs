using Godot;
using System;

public partial class ScoreLabel : Label
{
	// Called when the node enters the scene tree for the first time.
	public knifeCharacter g;

	public override void _Ready()
	{
		g = (knifeCharacter)GetNode("/root/knifeCharacter");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var number = g.clickCounter;
		GD.Print(number);
		this.Text = $"Score: is not working :(";
		//GD.Print($"BladeContactState: {bladeContactState}, BladeSpreadState: {bladeSpreadState}, KnifeAreaContact: {knifeAreaContact}");

	}
}
