using Godot;
using System;
using System.Data.Common;
using System.Runtime.Serialization;

public partial class knifeCharacter : CharacterBody2D
{
	Vector2 throwVector;
	float throwForce;
	RigidBody2D rigidBody;
	Line2D line;
	private Texture2D knifeWithButter;
	

//////////////////////////////////////////
//////////// VARIABLES ///////////////////
//////////////////////////////////////////
	bool mouseWasReleased = true;

	bool isInAir = false;
	
	float FallProgress = 0f;

	int Gravity = 100;

	bool stuckToWall = false;
	bool isRotating = false;
	bool isHandleAreaContact = false;

	bool ifJamOn = false;
	bool ifPeanutOn = false;

	bool isBladeOnNonStick = false;

	bool isBladeOnNormal = false;


////////////////////////////////////////////////////
//////////// SIGNALS FUNCTIONS /////////////////////
////////////////////////////////////////////////////
	public void _on_blade_area_body_entered(Node obj)
	{
		 GD.Print(this.Name + " blade collided with " + obj.Name);
		 if (obj is TileMap)
		{
			// Get the cell coordinates where the collision occurred
			TileMap body = (TileMap) obj;
			var collision = GetSlideCollision(0);
			if (collision == null) {
				GD.Print("NULLCASE");
				isBladeOnNormal = true;
				return;
			}
			Vector2 colpos = collision.GetPosition();

			colpos = GetClosestCell(body, body.LocalToMap(colpos));
	
			//Vector2I cellPosition = body.LocalToMap(colpos);
			
			var customData = (TileData)body.GetCellTileData(0, (Vector2I)colpos);
			GD.Print(customData);
			// Check for the custom property in the tileset
			// int tileId = ((TileMap)body).GetCell(cellPosition);
			// TileSet tileset = ((TileMap)body).TileSet;
			bool touchedLava = (bool)customData.GetCustomData("Lava");
			bool touchedNonStick = (bool)customData.GetCustomData("NonStick");
			bool touchedNormal = (bool)customData.GetCustomData("Normal");
			
			if (touchedLava)
			{
				// Game over logic
				GD.Print("Game Over!");
				// GetTree().ReloadCurrentScene();
				GetNode<Sprite2D>("KnifeSprite").Texture = knifeWithButter;
				ifPeanutOn = true;
			}
			if (touchedNonStick) {
				GD.Print("NonStick");
				isBladeOnNonStick = true;
			}
			if (touchedNormal) {
				GD.Print("Normal");
				isBladeOnNormal = true;
			}
			else {
				GD.Print("Normal Backup");
				isBladeOnNormal = true;
			}
		} else {
			isBladeOnNormal = true;
		}
	}
	
	public void _on_blade_area_body_exited(Node obj) {
		GD.Print(this.Name + " handle exited from " + obj.Name);
		resetCollisionVariables();
	}


	public void _on_handle_area_body_entered(Node obj)
	{
		GD.Print(this.Name + " handle collided with " + obj.Name);
		isHandleAreaContact = true;
	}
	// dasdsad
	private void _on_handle_area_body_exited(Node obj)
	{
		GD.Print(this.Name + " handle exited from " + obj.Name);
		isHandleAreaContact = false;
	}

/////////////////////////////////////////////////
//////////// MAIN FUNCTIONS /////////////////////
/////////////////////////////////////////////////
	public override void _Ready()
	{
		rigidBody = GetNode<RigidBody2D>("RigidBody2D");
		line = GetNode<Line2D>("Line2D");
		rigidBody.GravityScale = 0;
		knifeWithButter = (Texture2D)ResourceLoader.Load("res://knifeButteredUp.png");
		ifJamOn = false;
		
	}

	public override void _Process(double delta)
	{
		// if mouse down do function

///////////////////// MOUSE MOVEMENT //////////////////////////
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


////////// HANDLE GRAVITY /////////////////////
		Vector2 velocity = Velocity;

		//test
		FallProgress += (float)delta;
		velocity.Y += FallProgress * Gravity * (float)delta*10;
		//Velocity = rigidBody.LinearVelocity;
		Velocity = velocity;
		
		MoveAndSlide();

////////// HANDLE ROTATION /////////////////////
		// Rotates The knife Sprite by 0.1f
		// && isInAir
		if (velocity.Length() > 0.1f) {
			//if (isRotating) 
			//{
			this.Rotate(Velocity.X/20*MathF.Sqrt(FallProgress*20)*(float)0.005f);

			//}
			// else 
			// {
			// 	await ToSignal(GetTree().CreateTimer(0.01), "timeout"); // Connect the timeout signal to the OnTimerTimeout method
			// 	isRotating = true;
			// }
		}

////////// HANDLE COLLISION /////////////////////
		var collision = GetSlideCollision(GetSlideCollisionCount() - 1);
		
		// isHandle -> Jam ->  
		//GD.Print($"isHandle: {isHandleAreaContact}, IsBladeOnNonStick: {isBladeOnNonStick}, IsBladeOnNormal: {isBladeOnNormal}");
		if (collision != null) {
			FallProgress = 0;
			// print isHandle, IsBladeOnNonStick, IsBladeOnNormal;
			Velocity = velocity;
			if (isHandleAreaContact) {	
				Velocity = Velocity.Bounce(collision.GetNormal())*(float)0.2;
			} else if (ifJamOn) {
				Velocity = Velocity.Bounce(collision.GetNormal())*(float)0.4;
			} else if (isBladeOnNonStick == true) {
				//velocity.X = 0;
				velocity.Y = 100;
				if (ifPeanutOn) 
				{
					velocity.Y = 0;
				}
				Velocity = velocity;
				//Velocity = Velocity.Slide(collision.GetNormal())*(float)0.2;
			} else if (isBladeOnNormal == true) {
				velocity.X = 0;
				velocity.Y = 0;
				Velocity = velocity;
				//Velocity = Velocity.Slide(collision.GetNormal())*(float)0.2;
			} 
		}
	}


/////////////////////////////////////////////////////////
//////////// MOVEMENT BASED HELPERS /////////////////////
/////////////////////////////////////////////////////////
	
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

/////////////////////////////////////////////////////////
//////////// COLLISION BASED HELPERS /////////////////////
/////////////////////////////////////////////////////////
	void resetCollisionVariables() {
		isBladeOnNonStick = false;
		//isHandleAreaContact = false;
		isBladeOnNormal = false;
		stuckToWall = false;
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


//////////////////////////////////////////
//////////// OTHER ///////////////////////
//////////////////////////////////////////
	public void OnTimerTimeoutRotate()
	{
		this.Rotate(0.1f);
	}

}



