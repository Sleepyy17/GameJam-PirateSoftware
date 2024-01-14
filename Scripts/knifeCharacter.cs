using Godot;
using System;

public partial class knifeCharacter : CharacterBody2D
{
	Vector2 throwVector;
	RigidBody2D rigidBody;
	Line2D line;

	bool mouseWasReleased = true;

	bool isInAir = false;
	
	float FallProgress = 0f;

	int Gravity = 100;


	public override void _Ready()
	{
		rigidBody = GetNode<RigidBody2D>("RigidBody2D");
		line = GetNode<Line2D>("Line2D");
		rigidBody.GravityScale = 0;
	}

	public override void _Process(double delta)
	{
		// if mouse down do function

		if (Input.IsMouseButtonPressed(MouseButton.Left))
		{
			if (mouseWasReleased)
			{
				// Mouse down event
				OnMouseDown();
				mouseWasReleased = false;
			}
			else
			{
				// Mouse drag event
				OnMouseDrag();
			}
		}
		else if (!mouseWasReleased)
		{
			// Mouse release event
			OnMouseUp();
			mouseWasReleased = true;
		}


		Vector2 velocity = Velocity;
		if (IsOnFloor() || IsOnWall()) {
			FallProgress = 0;
			if (isInAir == false)
			{
				velocity.X = 0;
			}
			isInAir = false;
		}
		FallProgress += (float)delta;
		velocity.Y += FallProgress * Gravity * (float)delta*10;
		//Velocity = rigidBody.LinearVelocity;
		Velocity = velocity;
		MoveAndSlide();
	}
	void OnMouseDown()
	{
		CalculateThrowVector();
		SetArrow();
	}

	void OnMouseDrag()
	{
		CalculateThrowVector();
		SetArrow();
	}

	void CalculateThrowVector()
	{
		Vector2 mousePosition = GetGlobalMousePosition();
		Vector2 position = Position;
		throwVector = -(mousePosition - position).Normalized() * 100;
		GD.Print(throwVector);

	}

	void SetArrow()
	{
		line.Points = new Vector2[] { Vector2.Zero, throwVector * 100 };
	}

	void OnMouseUp()
	{
		ThrowKnife();
		isInAir = true;
		GD.Print("Mouse up");
	}

	void ThrowKnife()
	{
		// add velocity to the knife
		float throwForce = 1f;
    	//rigidBody.ApplyImpulse(Vector2.Zero, throwVector * throwForce);
		Velocity = throwVector * throwForce*5;
	}

}
