using Godot;

namespace Teoti.Core;

public class Logger
{
    public void Log(string tag, string message, params object[] args)
    {
        GD.Print(tag, " ", string.Format(message, args));
    }
}