using Godot;
using System;

public partial class knifeCharacter : CharacterBody2D
{
	Vector2 throwVector;
	float throwForce;
	RigidBody2D rigidBody;
	Line2D line;
	private Texture2D knifeWithButter;

	bool mouseWasReleased = true;

	bool isInAir = false;
	
	float FallProgress = 0f;

	int Gravity = 100;

	bool stuckToWall = false;
	bool isRotating = false;


	public void _on_blade_area_body_entered(TileMap body)
	{
		 GD.Print(this.Name + " blade collided with " + body.Name);
		 if (body is TileMap)
		{
			// Get the cell coordinates where the collision occurred
			var collision = GetSlideCollision(0);
			Vector2 colpos = collision.GetPosition();
			colpos = GetClosestCell(body, body.LocalToMap(colpos));
	
			//Vector2I cellPosition = body.LocalToMap(colpos);
			
			var customData = (TileData)body.GetCellTileData(0, (Vector2I)colpos);
			GD.Print(customData);
			// Check for the custom property in the tileset
			// int tileId = ((TileMap)body).GetCell(cellPosition);
			// TileSet tileset = ((TileMap)body).TileSet;
			bool customDataBool = (bool)customData.GetCustomData("Lava");
			GD.Print(customDataBool);
			
			if (customDataBool)
			{
				// Game over logic
				GD.Print("Game Over!");
				// GetTree().ReloadCurrentScene();
				GetNode<Sprite2D>("KnifeSprite").Texture = knifeWithButter;
				
				
			}
		}
	}

	public Vector2I GetClosestCell(TileMap tileMap, Vector2 position)
	{
		Vector2I closestCell = new Vector2I();
		float smallestDistance = float.MaxValue;

		foreach (Vector2I cell in tileMap.GetUsedCells(0))
		{
			float distance = position.DistanceTo(cell);

			if (distance < smallestDistance)
			{
		   		smallestDistance = distance;
				closestCell = cell;
			}
		}
		GD.Print(closestCell);
		return closestCell;
	}

	public void _on_handle_area_body_entered(TileMap body)
	{
		 GD.Print(this.Name + " handle collided with " + body.Name);
	}

	public override void _Ready()
	{
		rigidBody = GetNode<RigidBody2D>("RigidBody2D");
		line = GetNode<Line2D>("Line2D");
		rigidBody.GravityScale = 0;
		knifeWithButter = (Texture2D)ResourceLoader.Load("res://knifeButteredUp.png");
		
	}

	public override async void _Process(double delta)
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




		//test
		FallProgress += (float)delta;
		velocity.Y += FallProgress * Gravity * (float)delta*10;
		//Velocity = rigidBody.LinearVelocity;
		Velocity = velocity;
		
		MoveAndSlide();
		// Rotates The knife Sprite by 0.1f

		// && isInAir
		if (velocity.Length() > 0.1f) {
			//if (isRotating) 
			//{
				this.Rotate(Velocity.X/10*(float)0.005f);

			//}
			// else 
			// {
			// 	await ToSignal(GetTree().CreateTimer(0.01), "timeout"); // Connect the timeout signal to the OnTimerTimeout method
			// 	isRotating = true;
			// }
		}
		var collision = GetSlideCollision(GetSlideCollisionCount() - 1);
		if (collision != null) {
			FallProgress = 0;
			if (isInAir == false)
			{
				velocity.X = 0;
				//velocity.Y = 10;
				
			}
			isInAir = false;
			isRotating = false;
			Velocity = velocity;
			Velocity = Velocity.Slide(collision.GetNormal())*(float)0.2;
		}

		
		
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

	public void CalculateThrowVector()
	{
		Vector2 mousePosition = GetGlobalMousePosition();
		Vector2 position = Position;
		Vector2 relativePosition = mousePosition - position;
		throwForce = (mousePosition - position).Length();
		if (throwForce > 300f) throwForce = 300f;
		throwVector = -(mousePosition - position).Normalized();

	}

	void SetArrow()
	{
		line.Points = new Vector2[] { Vector2.Zero, throwVector * throwForce };
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
		//rigidBody.ApplyImpulse(Vector2.Zero, throwVector * throwForce);
		Velocity = throwVector * throwForce * 2;
		//this.Rotate(0.1f);
	
	
	}
	public void OnTimerTimeoutRotate()
	{
		this.Rotate(0.1f);
	}
}
