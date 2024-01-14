using Godot;
using System;

public partial class KnifeSprite : Sprite2D
{
	Vector2 throwVector;
	// Rotation speed in degrees per second
	private float rotationSpeed = 90.0f; // Adjust this value as needed

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Initialization code here
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
		Vector2 mousePosition = GetGlobalMousePosition();
		Vector2 localPosition = Position;
	
		if (throwVector[0] > 0) {
			RotationDegrees += (float)(rotationSpeed * delta);
		} else {
			RotationDegrees += (float)(-rotationSpeed * delta);
		}

	}
}
