using Godot;
using System;


public partial class testPlayer : CharacterBody2D
{
	[Export]
    public int Speed { get; set; } = 2000;
	public int Gravity { get; set; } = 20000;
	public float Friction { get; set; } = 0.1f;
	public float Acceleration { get; set; } = 0.5f;
	public int JumpHeight { get; set; } = 5000;
	public float JumpProgress { get; set; } = 0f;
	public bool IsJumping { get; set; } = false;
	public float FallProgress { get; set; } = 0f;
	//public bool isFalling { get; set; } = false;

	public Vector2 velocity = new Vector2();

	public void PlayerJump(float delta, Vector2 velocity) {
		GD.Print("JumpProgress: " + delta);
		JumpProgress += delta;
        float t = 0.25f - JumpProgress;
        velocity.Y = -JumpHeight * (0.25f - (0.25f - t));
		if (JumpProgress >= 0.25f) {
			IsJumping = false;
			FallProgress = 0;
		}
		return;
	}

	public void PlayerMove(float delta, int direction) {
		if (direction != 0) {
			//GD.Print("direction: " + direction);
			velocity.X = Mathf.Lerp(velocity.X, direction * Speed, Acceleration * delta);
		}
		else {
			velocity.X = Mathf.Lerp(velocity.X, 0, Friction * delta);
		}
		return;
	}

	public void PlayerFall(float delta) {
		FallProgress += delta;
		velocity.Y += FallProgress * Gravity * delta;
		return;
	}

    public void GetInput(float delta) {
		velocity = Velocity;

		int direction = 0; 
		if (Input.IsActionPressed("left")) {
			direction -= 1;
		}
		if (Input.IsActionPressed("right")) {
			direction += 1;
		}		
		PlayerMove(delta, direction);
		// jump
		if (Input.IsActionJustPressed("up") && (IsOnFloor() || IsOnWall()) && !IsJumping) {
        	IsJumping = true;
        	JumpProgress = 0f;
    	}

    	if (IsJumping) {
        	PlayerJump(delta, velocity);
			GD.Print("JumpProgress: " + JumpProgress + " velocity.Y: " + velocity.Y);
		} else {
			// gravity
			PlayerFall(delta);
		}
    	Velocity = velocity;


		// if (Input.IsActionJustPressed("up") && (IsOnFloor() || IsOnWall())) {
		// 	//make a smooth jump by lerping the velocity
			

		// 	velocity.Y = -JumpHeight;
		// }

		// // gravity
		// velocity.Y += Gravity * delta;
		// Velocity = velocity;
		return;
    }

    public override void _PhysicsProcess(double delta) {

        GetInput((float)delta);
        MoveAndSlide();
    }
}

/*public partial class testPlayer : CharacterBody2D
{
	[Export]
    public int Speed { get; set; } = 400;

    public void GetInput() {

        Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
        Velocity = inputDirection * Speed;
    }

    public override void _PhysicsProcess(double delta) {
        GetInput();
        MoveAndSlide();
    }
}*/