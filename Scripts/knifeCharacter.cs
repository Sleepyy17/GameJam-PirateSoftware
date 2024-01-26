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
	private Texture2D knifeWIthPeanut;
	private Texture2D knifeWithJam;
	
	public float clickCounter = 0;

//////////////////////////////////////////
//////////// VARIABLES ///////////////////
//////////////////////////////////////////
	bool mouseWasReleased = true;

	bool isInAir = false;
	
	float FallProgress = 0f;

	int Gravity = 100;

	bool stuckToWall = false;
	bool isRotating = false;
	


	public enum KnifeAreaContacts {
		Blade,
		Handle,
	}
	KnifeAreaContacts knifeAreaContact = KnifeAreaContacts.Handle;

	public enum BladeSpreadStates {
		NoSpread,
		JamOn,
		PeanutOn,
	}

	BladeSpreadStates bladeSpreadState = BladeSpreadStates.NoSpread;

	public enum BladeContactStates {
		Normal,
		OnNonStick,
		Death,
	}
	
	BladeContactStates bladeContactState = BladeContactStates.Normal;
	

	bool moveable = false;


////////////////////////////////////////////////////
//////////// SIGNALS FUNCTIONS /////////////////////
////////////////////////////////////////////////////
	public void _on_blade_area_body_entered(Node obj)
	{
		GD.Print(this.Name + " blade collided with " + obj.Name);
		if (obj is TileMap) {
			knifeAreaContact = KnifeAreaContacts.Blade;
		}
		
	}
	
	public void _on_blade_area_body_exited(Node obj) {
		GD.Print(this.Name + " handle exited from " + obj.Name);
		if (obj is TileMap) {
			resetCollisionVariables();
			//knifeAreaContact = KnifeAreaContacts.Handle;
		}
	}

	public void _on_handle_area_body_entered(Node obj)
	{
		GD.Print(this.Name + " handle collided with " + obj.Name);
		if (obj is TileMap) {
			knifeAreaContact = KnifeAreaContacts.Handle;
		}
	}
	// dasdsad
	private void _on_handle_area_body_exited(Node obj)
	{
		GD.Print(this.Name + " handle exited from " + obj.Name);
		if (obj is TileMap) {
			//knifeAreaContact = KnifeAreaContacts.Blade;
		}
	}

	   public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("restart"))
		{
			GetTree().ReloadCurrentScene();
		}
	}

/////////////////////////////////////////////////
//////////// MAIN FUNCTIONS /////////////////////
/////////////////////////////////////////////////
	public override void _Ready()
	{
		rigidBody = GetNode<RigidBody2D>("RigidBody2D");
		line = GetNode<Line2D>("Line2D");
		rigidBody.GravityScale = 0;
		knifeWIthPeanut = (Texture2D)ResourceLoader.Load("res://knifeButteredUp.png");
		knifeWithJam = (Texture2D)ResourceLoader.Load("res://knifeWithJam.png");
		clickCounter = 0;
	}

	public override void _Process(double delta)
	{	
		Sprite2D knifeSprite = this.GetNode("KnifeSprite") as Sprite2D;
		if (knifeSprite.Texture == knifeWithJam) {
			bladeSpreadState = BladeSpreadStates.JamOn;
		}
		if (knifeSprite.Texture == knifeWIthPeanut) {
			bladeSpreadState = BladeSpreadStates.PeanutOn;
		}
		//GD.Print(ifJamOn);
		// if mouse down do function

///////////////////// MOUSE MOVEMENT //////////////////////////
		if (Input.IsMouseButtonPressed(MouseButton.Left) && moveable)
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
		if (!IsOnFloor()) {
			FallProgress += (float)delta;
			velocity.Y += FallProgress * Gravity * (float)delta*10;
		}
		//Velocity = rigidBody.LinearVelocity;
		Velocity = velocity;
		
		MoveAndSlide();

////////// HANDLE ROTATION /////////////////////
		// Rotates The knife Sprite by 0.1f
		// && isInAir
		if (velocity.Length() > 0.1f) {
			//if (isRotating) 
			//{
			this.Rotate((Velocity.X/18+(Velocity.Sign().X*MathF.Sqrt(FallProgress*20)))*(float)0.005f);

			//}
			// else 
			// {
			// 	await ToSignal(GetTree().CreateTimer(0.01), "timeout"); // Connect the timeout signal to the OnTimerTimeout method
			// 	isRotating = true;
			// }
		}

////////// HANDLE COLLISION /////////////////////
///
		var collision = GetSlideCollision(0); // Get the most recent collision
		TileMap body = (TileMap)collision.GetCollider(); // Get the collider
		// GD.Print("I collided with ", ((Node)collision.GetCollider()).Name);
		var collisionName = ((Node)collision.GetCollider()).Name;
		Vector2 colpos = collision.GetPosition();

		colpos = GetClosestCell(body, body.LocalToMap(colpos));
	
			//Vector2I cellPosition = body.LocalToMap(colpos);
			
		var customData = (TileData)body.GetCellTileData(0, (Vector2I)colpos);
			//GD.Print("herllo" + customData);
			// Check for the custom property in the tileset
			// int tileId = ((TileMap)body).GetCell(cellPosition);
			// TileSet tileset = ((TileMap)body).TileSet;
		bool touchedLava = (bool)customData.GetCustomData("Lava");
		bool touchedNonStick = (bool)customData.GetCustomData("NonStick");
		bool touchedNormal = (bool)customData.GetCustomData("Normal");
			
			
		if (touchedLava)
		{
			// Game over logic
			//GD.Print("Game Over!");
			// GetTree().ReloadCurrentScene();
			GetNode<Sprite2D>("KnifeSprite").Texture = knifeWIthPeanut;
			bladeContactState = BladeContactStates.Death;

		}
		if (touchedNonStick) {
			//GD.Print("NonStick");
			bladeContactState = BladeContactStates.OnNonStick;
		}
		if (touchedNormal) {
				//GD.Print("Normal");
			bladeContactState = BladeContactStates.Normal;
		}
		
		// isHandle -> Jam ->  
		//GD.Print($"BladeContactState: {bladeContactState}, BladeSpreadState: {bladeSpreadState}, KnifeAreaContact: {knifeAreaContact}");
		if (collision != null) {
			moveable = true;
			// print isHandle, IsBladeOnNonStick, IsBladeOnNormal;
			FallProgress = 0;
			Velocity = velocity;
			if (knifeAreaContact == KnifeAreaContacts.Handle) {	
				//GD.Print(velocity);
				Velocity = Velocity.Bounce(GetSlideCollision(GetSlideCollisionCount() - 1).GetNormal())*(float)0.2;
				//GD.Print("BOUNCE");
				//GD.Print(velocity);

			} 
			// ONLY IF KNIFE IS ON BLADE SIDE
			else if (bladeSpreadState == BladeSpreadStates.JamOn) {
				Velocity = Velocity.Bounce(GetSlideCollision(GetSlideCollisionCount() - 1).GetNormal())*(float)0.4;
			} 
			// ONLY IF KNIFE IS ON BLADE SIDE AND DOES NOT HAVE JAM
			else if (bladeContactState == BladeContactStates.OnNonStick) {
				//velocity.X = 0;
				velocity.Y = 100;
				if (bladeSpreadState == BladeSpreadStates.PeanutOn) 
				{
					velocity.Y = 0;
				}
				Velocity = velocity;
				//Velocity = Velocity.Slide(collision.GetNormal())*(float)0.2;
			} else if (bladeContactState == BladeContactStates.Normal) {
				velocity.X = 0;
				velocity.Y = 0;
				Velocity = velocity;
				//Velocity = Velocity.Slide(collision.GetNormal())*(float)0.2;
			} 
		} else {
			moveable = false;
		}
		FallProgress = 0;
	}

	Vector2 handleCollision(Vector2 velocity, KinematicCollision2D collision) {
		//GD.Print($"isHandle: {isHandleAreaContact}, IsBladeOnNonStick: {isBladeOnNonStick}, IsBladeOnNormal: {isBladeOnNormal}");
		//GD.Print($"BladeContactState: {bladeContactState}, BladeSpreadState: {bladeSpreadState}, KnifeAreaContact: {knifeAreaContact}");
		FallProgress = 0;
			if (knifeAreaContact == KnifeAreaContacts.Handle) {	
		//GD.Print(velocity);
				velocity = velocity.Bounce(GetSlideCollision(0).GetNormal())*(float)1;
				GD.Print("BOUNCE");
				GD.Print(velocity);

			} 
			// ONLY IF KNIFE IS ON BLADE SIDE
			else if (bladeSpreadState == BladeSpreadStates.JamOn) {
				velocity = velocity.Bounce(GetSlideCollision(GetSlideCollisionCount() - 1).GetNormal())*(float)0.4;
			} 
			// ONLY IF KNIFE IS ON BLADE SIDE AND DOES NOT HAVE JAM
			else if (bladeContactState == BladeContactStates.OnNonStick) {
				//velocity.X = 0;
				velocity.Y = 100;
				if (bladeSpreadState == BladeSpreadStates.PeanutOn) 
				{
					velocity.Y = 0;
				}
				Velocity = velocity;
				//Velocity = Velocity.Slide(collision.GetNormal())*(float)0.2;
			} else if (bladeContactState == BladeContactStates.Normal) {
				velocity.X = 0;
				velocity.Y = 0;
				Velocity = velocity;
				//Velocity = Velocity.Slide(collision.GetNormal())*(float)0.2;
			} 

		return velocity;
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
		clickCounter += 1;
		//GD.Print("Mouse up");
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
		//isBladeOnNonStick = false;
		//isHandleAreaContact = false;
		//isBladeOnNormal = false;
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
		//GD.Print(closestCell);
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



