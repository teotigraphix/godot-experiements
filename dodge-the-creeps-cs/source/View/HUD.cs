using Godot;
using System;
using Timer = Godot.Timer;

namespace Teoti.View;

public partial class HUD : CanvasLayer
{
	[Signal]
	public delegate void StartTriggeredEventHandler();
	
	//-----------------------------------------------------------------------------
	// Private :: Variables
	//-----------------------------------------------------------------------------

	private Button startButton;
	private Timer messageTimer;
	private Label messageLabel;
	private Label scoreLabel;

	//-----------------------------------------------------------------------------
	// Overridden :: Methods
	//-----------------------------------------------------------------------------

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		startButton = GetNode<Button>("StartButton");
		messageTimer = GetNode<Timer>("MessageTimer");
		messageLabel = GetNode<Label>("Message");
		scoreLabel = GetNode<Label>("ScoreLabel");

		messageTimer.Timeout += () => { messageLabel.Hide(); };
		
		startButton.Pressed += () =>
		{
			startButton.Hide();
			EmitSignal(SignalName.StartTriggered);
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	//-----------------------------------------------------------------------------
	// API :: Methods
	//-----------------------------------------------------------------------------

	public void ShowMessage(string text)
	{
		messageLabel.Text = text;
		messageLabel.Show();

		messageTimer.Start();
	}

	public void UpdateScore(int score)
	{
		scoreLabel.Text = score.ToString();
	}
	
	public async void ShowGameOver()
	{
		ShowMessage("Game Over");

		await ToSignal(messageTimer, Timer.SignalName.Timeout);

		messageLabel.Text = "Dodge the Creeps";
		messageLabel.Show();

		await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
		startButton.Show();
	}
}
