using Godot;
using System;


public partial class testPlayer : CharacterBody2D
{
	[Export]
    public int Speed { get; set; } = 20000;
	public int Gravity { get; set; } = 30000;
	public float Friction { get; set; } = 0.1f;
	public float Acceleration { get; set; } = 0.5f;
	public int JumpHeight { get; set; } = 5000;
	public float JumpProgress { get; set; } = 0f;
	public bool IsJumping { get; set; } = false;
	public float FallProgress { get; set; } = 0f;
	//public bool isFalling { get; set; } = false;


	
// 	public Vector2 velocity = new Vector2();

// public void GetInput(float delta) {
//     if (IsOnFloor() || IsOnWall()) {
//         FallProgress = 0;
//     }

//     HandleMovement(delta);
//     HandleJump(delta);
//     HandleFall(delta);

//     Velocity = velocity;
// }

// private void HandleMovement(float delta) {
//     int direction = 0; 
//     if (Input.IsActionPressed("left")) {
//         direction -= 1;
//     }
//     if (Input.IsActionPressed("right")) {
//         direction += 1;
//     }       
//     if (direction != 0) {
//         velocity.X = Mathf.Lerp(velocity.X, direction * Speed, Acceleration * delta);
//     }
//     else {
//         velocity.X = Mathf.Lerp(velocity.X, 0, Friction * delta);
//     }
// }

// private void HandleJump(float delta) {
//     if (Input.IsActionJustPressed("up") && (IsOnFloor() || IsOnWall()) && !IsJumping) {
//         IsJumping = true;
//         JumpProgress = 0f;
//     }

//     if (IsJumping) {
//         JumpProgress += delta;
//         float t = 0.25f - JumpProgress;
//         velocity.Y = -JumpHeight * (0.25f - (0.25f - t));
//         if (JumpProgress >= 0.25f) {
//             IsJumping = false;
//             FallProgress = 0;
//         }
//     }
// }

// private void HandleFall(float delta) {
//     if (!IsJumping) {
//         FallProgress += delta;
//         velocity.Y += FallProgress * Gravity * delta * 4;
//     }
// }

//     public override void _PhysicsProcess(double delta) {

//         GetInput((float)delta);
// 		GD.Print("Velocity: " + Velocity);
//         MoveAndSlide();
//     }
// }


/////////////////////////////////////////////////////////////////
///

    public void GetInput(float delta) {

		if (IsOnFloor() || IsOnWall()) {
			FallProgress = 0;
		}
		Vector2 velocity = new Vector2();
		int direction = 0; 
		if (Input.IsActionPressed("left")) {
			direction -= 1;
		}
		if (Input.IsActionPressed("right")) {
			direction += 1;
		}		
		if (direction != 0) {
			//GD.Print("direction: " + direction);
			velocity.X = Mathf.Lerp(velocity.X, direction * Speed, Acceleration * delta);
		}
		else {
			velocity.X = Mathf.Lerp(velocity.X, 0, Friction * delta);
		}
		// jump
		if (Input.IsActionJustPressed("up") && (IsOnFloor() || IsOnWall()) && !IsJumping) {
        	IsJumping = true;
        	JumpProgress = 0f;
    	}

    	if (IsJumping) {
        	JumpProgress += delta;
        	float t = 0.25f - JumpProgress;
        	velocity.Y = -JumpHeight * (0.25f - (0.25f - t));
			if (JumpProgress >= 0.25f) {
				IsJumping = false;
				FallProgress = 0;
			}
		} else {
			// gravity
			
			FallProgress += delta;
			velocity.Y += FallProgress * Gravity * delta * 4;

		}
    	Velocity = velocity;
    }


    public override void _PhysicsProcess(double delta) {

        GetInput((float)delta);
        MoveAndSlide();
    }
}
