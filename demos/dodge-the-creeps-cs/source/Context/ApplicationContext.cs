using Godot;
using Teoti.App;
using Teoti.Context;
using Teoti.Mediators;
using Teoti.View;

namespace Teoti.Core;

public partial class ApplicationContext : GodotObject
{
    //-----------------------------------------------------------------------------
    // Private :: Variables
    //-----------------------------------------------------------------------------

    private Main _root;

    private Logger _logger;
    private TimerManager _timerManager;

    private GameModel _model;
    private GameService _service;
    private GameController _controller;

    private InputContext _input;
    private AudioContext _audio;

    private PlayerContext _player;
    private EnemyContext _enemy;

    private UIContext _ui;

    //-----------------------------------------------------------------------------
    // API :: Properties
    //-----------------------------------------------------------------------------

    public Logger Logger => _logger;
    public TimerManager TimerManager => _timerManager;

    public GameModel Model => _model;
    public GameService Service => _service;
    public GameController Controller => _controller;

    public InputContext Input => _input;
    public AudioContext Audio => _audio;

    public PlayerContext Player => _player;
    public EnemyContext Enemy => _enemy;

    public UIContext UI => _ui;
    
    protected Main Root
    {
        get => _root;
        private set => _root = value;
    }

    //-----------------------------------------------------------------------------
    // Constructors
    //-----------------------------------------------------------------------------

    public ApplicationContext(Main root)
    {
        _root = root;
    }

    //-----------------------------------------------------------------------------
    // API :: Methods
    //-----------------------------------------------------------------------------

    public void Init()
    {
        _logger = new Logger();

        _model = new GameModel();
        _service = new GameService(this);
        _controller = new GameController(this);

        _input = new InputContext(Service);

        _player = new PlayerContext(
            Service,
            Root.GetNode<Player>("Player"),
            Root.GetNode<Marker2D>("StartPosition"));

        _enemy = new EnemyContext(this,
            Root.EnemyScenePrefab,
            Root.GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation"));

        _audio = new AudioContext(
            Root.GetNode<AudioStreamPlayer>("Music"),
            Root.GetNode<AudioStreamPlayer>("DeathSound"));

        _ui = new UIContext(_root,
            Root.GetNode<HUD>("Hud"),
            Root.GetNode<ColorRect>("ColorRect"));

        _timerManager = new TimerManager(
            _root.GetNode<Timer>("StartTimer"),
            _root.GetNode<Timer>("ScoreTimer"),
            _root.GetNode<Timer>("EnemyTimer"));

        Input.Init();
        Model.Init();
        Service.Init();
        Controller.Init();

        // Mediators
        var playerMediator = new PlayerMediator(Service, Player);
        var mobMediator = new EnemyMediator(Service, Enemy);
    }
}