using Godot;
using Teoti.Context;
using Teoti.Core;

namespace Teoti.App;

public class GameService
{
    public const string TAG = "GameService";

    //-----------------------------------------------------------------------------
    // Private :: Variables
    //-----------------------------------------------------------------------------

    private readonly ApplicationContext _applicationContext;

    //-----------------------------------------------------------------------------
    // Constructors
    //-----------------------------------------------------------------------------

    public GameService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    //-----------------------------------------------------------------------------
    // API :: Methods
    //-----------------------------------------------------------------------------

    public void Init()
    {
    }

    //-----------------------------------------------------------------------------
    // Command :: Methods
    //-----------------------------------------------------------------------------

    /// <summary>
    /// These are Commands that can emit signals throughout the lifecycle
    /// </summary>
    public void NewGame()
    {
        _applicationContext.Logger.Log(TAG, $"NewGame()");

        new NewGameCommand(_applicationContext).Execute();
    }

    public void GameOver()
    {
        _applicationContext.Logger.Log(TAG, $"GameOver()");

        new GameOverCommand(_applicationContext).Execute();
    }

    public void SetScore(int score)
    {
        _applicationContext.Logger.Log(TAG, $"SetScore({score})");

        new SetScoreCommand(_applicationContext, score).Execute();
    }

    public void SpawnEnemy()
    {
        _applicationContext.Logger.Log(TAG, $"SpawnEnemy()");

        new SpawnCommand(_applicationContext).Execute();
    }

    public void NextPhase()
    {
        _applicationContext.Logger.Log(TAG, $"NextPhase()");

        new NextPhaseCommand(_applicationContext).Execute();
    }
}

//---------------------------------------------------------------------------------
// Commands
//---------------------------------------------------------------------------------

abstract class ApplicationCommand : Command
{
    protected ApplicationContext ApplicationContext { get; }

    public ApplicationCommand(ApplicationContext applicationContext)
    {
        ApplicationContext = applicationContext;
    }
}

class SpawnCommand : ApplicationCommand
{
    public SpawnCommand(ApplicationContext applicationContext) : base(applicationContext)
    {
    }

    public override void Execute()
    {
        ApplicationContext.Enemy.SpawnEnemy();
    }
}

class SetScoreCommand : ApplicationCommand
{
    private readonly int _score;

    public SetScoreCommand(ApplicationContext applicationContext, int score) : base(applicationContext)
    {
        _score = score;
    }

    public override void Execute()
    {
        ApplicationContext.Model.Score = _score;
    }
}

class NewGameCommand : ApplicationCommand
{
    public NewGameCommand(ApplicationContext applicationContext) : base(applicationContext)
    {
    }

    public override void Execute()
    {
        ApplicationContext.Model.Score = 0;

        ApplicationContext.Player.Start();

        ApplicationContext.Audio.PlaySFX(AudioContext.SFXType.Music, true);

        ApplicationContext.UI.CallGroup("mobs", Node.MethodName.QueueFree);
        ApplicationContext.UI.ShowMessage("Get Ready!");

        ApplicationContext.TimerManager.Start();
    }
}

class GameOverCommand : ApplicationCommand
{
    public GameOverCommand(ApplicationContext applicationContext) : base(applicationContext)
    {
    }

    public override void Execute()
    {
        ApplicationContext.UI.ShowGameOver();

        ApplicationContext.Audio.PlaySFX(AudioContext.SFXType.Music, false);
        ApplicationContext.Audio.PlaySFX(AudioContext.SFXType.Death, true);

        ApplicationContext.TimerManager.Stop();
    }
}

class NextPhaseCommand : ApplicationCommand
{
    public NextPhaseCommand(ApplicationContext applicationContext) : base(applicationContext)
    {
    }

    public override void Execute()
    {
        ApplicationContext.UI.NextBackgroundColor();

        ApplicationContext.Model.Score += 1;
    }
}