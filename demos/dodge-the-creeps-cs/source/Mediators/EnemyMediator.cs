using Teoti.App;
using Teoti.Context;

namespace Teoti.Mediators;

public class EnemyMediator
{
    private readonly GameService _gameService;
    private readonly EnemyContext _enemyContext;

    //-----------------------------------------------------------------------------
    // Constructors
    //-----------------------------------------------------------------------------

    public EnemyMediator(GameService gameService, EnemyContext enemyContext)
    {
        _gameService = gameService;
        _enemyContext = enemyContext;
    }
}