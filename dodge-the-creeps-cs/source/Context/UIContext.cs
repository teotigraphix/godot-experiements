using System;
using Godot;
using Teoti.View;

namespace Teoti.Context;

public partial class UIContext : GodotObject
{
    //-----------------------------------------------------------------------------
    // API :: Signals
    //-----------------------------------------------------------------------------

    [Signal]
    public delegate void StartTriggeredEventHandler();

    //-----------------------------------------------------------------------------
    // Private :: Variables
    //-----------------------------------------------------------------------------

    private readonly Main _root;
    private readonly HUD _hud;
    private readonly ColorRect _colorRect;

    //-----------------------------------------------------------------------------
    // Constructors
    //-----------------------------------------------------------------------------

    public UIContext(Main root, HUD hud, ColorRect colorRect)
    {
        _root = root;
        _hud = hud;
        _colorRect = colorRect;

        _hud.StartTriggered += () => { EmitSignal(SignalName.StartTriggered); };
    }

    //-----------------------------------------------------------------------------
    // API :: Methods
    //-----------------------------------------------------------------------------

    public void ShowGameOver()
    {
        _hud.ShowGameOver();
    }

    public void UpdateScore(int score)
    {
        _hud.UpdateScore(score);
    }

    public void ShowMessage(string message)
    {
        _hud.ShowMessage(message);
    }

    public void AddRootChild(Node child)
    {
        // XXX Mediator stuff
        _root.AddChild(child);
    }

    public void AddChild(Node parent, Node child)
    {
        // XXX Mediator stuff
        parent.AddChild(child);
    }

    public void CallGroup(string groupName, string methodName)
    {
        _root.GetTree().CallGroup(groupName, methodName);
    }

    public void NextBackgroundColor()
    {
        _colorRect.Color = new Color(GD.Randf(), GD.Randf(), GD.Randf());
    }
}