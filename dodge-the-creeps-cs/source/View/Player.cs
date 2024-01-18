using Godot;

namespace Teoti.View;

public partial class Player : Area2D
{
	//-----------------------------------------------------------------------------
	// API :: Signals
	//-----------------------------------------------------------------------------

	[Signal]
	public delegate void CollisionEventHandler();

	//-----------------------------------------------------------------------------
	// API :: Properties
	//-----------------------------------------------------------------------------

	// Using the export keyword on the first variable speed allows us
	// to set its value in the Inspector.
	[Export] public int Speed { get; set; } = 400;

	//-----------------------------------------------------------------------------
	// Private :: Variables
	//-----------------------------------------------------------------------------

	public Vector2 ScreenSize;
	private CollisionShape2D collisionShape2D;
	private AnimatedSprite2D animatedSprite2D;

	//-----------------------------------------------------------------------------
	// API :: Methods
	//-----------------------------------------------------------------------------

	public void Start(Vector2 position)
	{
		Position = position;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}

	//-----------------------------------------------------------------------------
	// Overridden :: Methods
	//-----------------------------------------------------------------------------

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();

		collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		ScreenSize = GetViewportRect().Size;

		BodyEntered += OnBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero;

		if (Input.IsActionPressed("ui_right"))
		{
			velocity.X += 1;
		}

		if (Input.IsActionPressed("ui_left"))
		{
			velocity.X -= 1;
		}

		if (Input.IsActionPressed("ui_down"))
		{
			velocity.Y += 1;
		}

		if (Input.IsActionPressed("ui_up"))
		{
			velocity.Y -= 1;
		}

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		}

		Position += velocity * (float) delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y));

		if (velocity.X != 0)
		{
			animatedSprite2D.Animation = "walk";
			animatedSprite2D.FlipV = false;
			animatedSprite2D.FlipH = velocity.X < 0;
		}
		else if (velocity.Y != 0)
		{
			animatedSprite2D.Animation = "up";
			animatedSprite2D.FlipV = velocity.Y > 0;
		}
	}

	//-----------------------------------------------------------------------------
	// Private :: Handlers
	//-----------------------------------------------------------------------------

	private void OnBodyEntered(Node2D body)
	{
		Hide(); // Player disappears after being hit.
		EmitSignal(SignalName.Collision);
		// Must be deferred as we can't change physics properties on a physics callback.
		collisionShape2D.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}
}
