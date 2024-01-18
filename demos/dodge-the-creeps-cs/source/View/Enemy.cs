using Godot;

namespace Teoti.View;

public partial class Enemy : RigidBody2D
{
	public AnimatedSprite2D animatedSprite2D;
	public VisibleOnScreenNotifier2D visibleNotifier;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		visibleNotifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		visibleNotifier.ScreenExited += () => QueueFree();

		string[] mobTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
		animatedSprite2D.Play(mobTypes[GD.Randi() % mobTypes.Length]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
