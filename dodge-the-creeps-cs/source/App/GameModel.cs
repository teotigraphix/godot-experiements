using Godot;

namespace Teoti.App;

public partial class GameModel : GodotObject
{
    //-----------------------------------------------------------------------------
    // API :: Signals
    //-----------------------------------------------------------------------------

    [Signal]
    public delegate void ScoreChangedEventHandler();
   
    //-----------------------------------------------------------------------------
    // Private :: Variables
    //-----------------------------------------------------------------------------

    private int _score;

    //-----------------------------------------------------------------------------
    // API :: Properties
    //-----------------------------------------------------------------------------

    public int Score
    {
        get => _score;
        internal set
        {
            if (_score == value)
                return;
            _score = value;
            EmitSignal(SignalName.ScoreChanged);
        }
    }
    
    //-----------------------------------------------------------------------------
    // Constructors
    //-----------------------------------------------------------------------------

    public GameModel()
    {
    }

    public void Init()
    {
    }
}