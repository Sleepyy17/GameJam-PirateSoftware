using Godot;
using System;

/*public partial class testPlayer : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}*/

public partial class testPlayer : CharacterBody2D
{
	[Export]
    public int Speed { get; set; } = 10000;
	public int Gravity { get; set; } = 10000;
	public float Friction { get; set; } = 0.1f;
	public float Acceleration { get; set; } = 0.5f;
	public int JumpHeight { get; set; } = 3000;
	public float JumpProgress { get; set; } = 0f;
	public bool IsJumping { get; set; } = false;
    public void GetInput(float delta) {

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
			GD.Print("JumpProgress: " + JumpProgress + " velocity.Y: " + velocity.Y);
        if (JumpProgress >= 0.25f) {
            IsJumping = false;
        }
		} else {
			// gravity
			velocity.Y += Gravity * delta;
		}
    	Velocity = velocity;


		// if (Input.IsActionJustPressed("up") && (IsOnFloor() || IsOnWall())) {
		// 	//make a smooth jump by lerping the velocity
			

		// 	velocity.Y = -JumpHeight;
		// }

		// // gravity
		// velocity.Y += Gravity * delta;
		// Velocity = velocity;
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