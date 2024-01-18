using Godot;
using Timer = Godot.Timer;

namespace Teoti.Core;

public partial class TimerManager : Node
{
    //-----------------------------------------------------------------------------
    // API :: Signals
    //-----------------------------------------------------------------------------
    
    [Signal]
    public delegate void StartCompleteEventHandler();

    [Signal]
    public delegate void PhaseCompleteEventHandler();
    
    [Signal]
    public delegate void SpawnEnemyEventHandler();
    
    //-----------------------------------------------------------------------------
    // Private :: Variables
    //-----------------------------------------------------------------------------
    
    private Timer _enemyTimer;
    private Timer _startTimer;
    private Timer _phaseTimer;

    //-----------------------------------------------------------------------------
    // Constructors
    //-----------------------------------------------------------------------------

    public TimerManager(Timer startTimer, Timer phaseTimer, Timer enemyTimer)
    {
        _startTimer = startTimer;
        _phaseTimer = phaseTimer;
        _enemyTimer = enemyTimer;

        // _startTimer = new Timer();
        // _startTimer.WaitTime = 2;
        // _startTimer.OneShot = true;
        //
        // _phaseTimer = new Timer();
        // _phaseTimer.WaitTime = 1;
        //
        // _enemyTimer = new Timer();
        // _enemyTimer.WaitTime = 0.5;
        
        //_startTimer.Timeout += OnStartTimerTimeout;
        _phaseTimer.Timeout += OnPhaseTimerTimeOut;
        _enemyTimer.Timeout += OnEnemyTimerTimeOut;
    }

    //-----------------------------------------------------------------------------
    // API :: Methods
    //-----------------------------------------------------------------------------

    public void Start()
    {
        GD.Print("TimerManager.Start");
        if (_phaseTimer.TimeLeft > 0)
            _phaseTimer.Stop();
        if (_enemyTimer.TimeLeft > 0)
            _enemyTimer.Stop();
        
        _phaseTimer.Start();
        _enemyTimer.Start();
    }

    public void Stop()
    {
        GD.Print("TimerManager.Stop");

        _phaseTimer.Stop();
        _enemyTimer.Stop();
    }
    
    //-----------------------------------------------------------------------------
    // Private :: Handlers
    //-----------------------------------------------------------------------------
    
    private void OnPhaseTimerTimeOut()
    {
        EmitSignal(SignalName.PhaseComplete);
    }

    private void OnEnemyTimerTimeOut()
    {
        EmitSignal(SignalName.SpawnEnemy);
    }
}