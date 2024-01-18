using Godot;
using Teoti.App;
using Teoti.View;

namespace Teoti.Context;

public partial class PlayerContext : GodotObject
{
    //-----------------------------------------------------------------------------
    // Private :: Variables
    //-----------------------------------------------------------------------------

    private readonly GameService _gameService;
    private readonly Player _player;

    public Player Player => _player;

    private readonly Marker2D _startPosition;

    //-----------------------------------------------------------------------------
    // Constructors
    //-----------------------------------------------------------------------------

    public PlayerContext(GameService gameService, Player player, Marker2D startPosition)
    {
        _gameService = gameService;
        _player = player;
        _startPosition = startPosition;
        
        // If not using mediator, just listen to signals from context
        // Player.Collision += () => { GD.Print("PlayerContext Collision"); };
        // Player.Collision += () => { _gameService.GameOver(); };
    }

    //-----------------------------------------------------------------------------
    // API :: Methods
    //-----------------------------------------------------------------------------

    public void Start()
    {
        _player.Start(_startPosition.Position);
    }
}