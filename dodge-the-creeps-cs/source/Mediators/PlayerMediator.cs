using Godot;
using Teoti.App;
using Teoti.Context;

namespace Teoti.Mediators;

public class PlayerMediator
{
    private readonly GameService _gameService;
    private readonly PlayerContext _playerContext;

    //-----------------------------------------------------------------------------
    // Constructors
    //-----------------------------------------------------------------------------

    public PlayerMediator(GameService gameService, PlayerContext playerContext)
    {
        _gameService = gameService;
        _playerContext = playerContext;
        
        _playerContext.Player.Collision += () => { GD.Print("PlayerMediator Collision"); };
        _playerContext.Player.Collision += () => { _gameService.GameOver(); };
    }
}