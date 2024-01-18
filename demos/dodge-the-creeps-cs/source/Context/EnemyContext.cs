using Godot;
using Teoti.Core;
using Teoti.View;

namespace Teoti.Context;

public partial class EnemyContext : GodotObject
{
    public const string TAG = "EnemyContext";
    
    //-----------------------------------------------------------------------------
    // API :: Signals
    //-----------------------------------------------------------------------------

    [Signal]
    public delegate void CurrentEnemyChangedEventHandler();

    //-----------------------------------------------------------------------------
    // Private :: Variables
    //-----------------------------------------------------------------------------

    private readonly ApplicationContext _applicationContext;
    private readonly PackedScene _enemyScenePrefab;
    private readonly PathFollow2D _enemySpawnLocation;

    private Enemy _currentEnemy;
    
    public Enemy CurrentEnemy
    {
        get => _currentEnemy;
        internal set
        {
            if (_currentEnemy == value)
                return;
            _currentEnemy = value;
            EmitSignal(SignalName.CurrentEnemyChanged);
        }
    }
    
    //-----------------------------------------------------------------------------
    // Constructors
    //-----------------------------------------------------------------------------

    public EnemyContext(
        ApplicationContext applicationContext,
        PackedScene enemyScenePrefab,
        PathFollow2D enemySpawnLocation)
    {
        _applicationContext = applicationContext;
        _enemyScenePrefab = enemyScenePrefab;
        _enemySpawnLocation = enemySpawnLocation;
    }

    //-----------------------------------------------------------------------------
    // API :: Methods
    //-----------------------------------------------------------------------------
    
    public void SpawnEnemy()
    {
        _applicationContext.Logger.Log(TAG, $"SpawnEnemy()");
        
        var enemy = _enemyScenePrefab.Instantiate<Enemy>();

        _enemySpawnLocation.ProgressRatio = GD.Randf();

        float direction = _enemySpawnLocation.Rotation + Mathf.Pi / 2;

        enemy.Position = _enemySpawnLocation.Position;

        direction += (float) GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        enemy.Rotation = direction;

        var velocity = new Vector2((float) GD.RandRange(150.0, 250.0), 0);
        enemy.LinearVelocity = velocity.Rotated(direction);

        _applicationContext.UI.AddRootChild(enemy);

        CurrentEnemy = enemy;
    }
}