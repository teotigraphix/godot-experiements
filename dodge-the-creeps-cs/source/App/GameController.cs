using Teoti.Core;

namespace Teoti.App;

/**
 * Listens to app and ui level signals and calls model/service API.
 */
public class GameController
{
    private const string TAG = "GameController";

    //-----------------------------------------------------------------------------
    // Private :: Variables
    //-----------------------------------------------------------------------------

    private readonly ApplicationContext _applicationContext;

    //-----------------------------------------------------------------------------
    // Constructors
    //-----------------------------------------------------------------------------

    public GameController(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    //-----------------------------------------------------------------------------
    // API :: Methods
    //-----------------------------------------------------------------------------

    public void Init()
    {
        _applicationContext.Model.ScoreChanged += OnModelScoreChanged;
        _applicationContext.UI.StartTriggered += OnUIStartTriggered;

        _applicationContext.TimerManager.PhaseComplete += OnTimerManagerPhaseComplete;
        _applicationContext.TimerManager.SpawnEnemy += OnTimerManagerSpawnEnemy;
    }

    //-----------------------------------------------------------------------------
    // Private :: Handlers
    //-----------------------------------------------------------------------------

    //-----------------------------------
    // Model
    //-----------------------------------
    
    private void OnModelScoreChanged()
    {
        _applicationContext.Logger.Log(TAG, "OnModelScoreChanged");
        
        _applicationContext.UI.UpdateScore(_applicationContext.Model.Score);
    }

    //-----------------------------------
    // UI
    //-----------------------------------

    private void OnUIStartTriggered()
    {
        _applicationContext.Logger.Log(TAG, "OnUIStartTriggered");
        
        _applicationContext.Service.NewGame();
    }

    //-----------------------------------
    // Timers
    //-----------------------------------

    private void OnTimerManagerPhaseComplete()
    {
        _applicationContext.Logger.Log(TAG, "OnPhaseComplete");

        _applicationContext.Service.NextPhase();
    }

    private void OnTimerManagerSpawnEnemy()
    {
        _applicationContext.Logger.Log(TAG, "OnSpawnEnemy");

        _applicationContext.Service.SpawnEnemy();
    }
}