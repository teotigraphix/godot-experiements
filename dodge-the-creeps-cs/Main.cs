using Godot;
using Teoti.Core;

public partial class Main : Node
{
    [Export] public PackedScene EnemyScenePrefab { get; set; }

    private ApplicationContext _applicationContext;

    public override void _Ready()
    {
        _applicationContext = new ApplicationContext(this);

        _applicationContext.Init();
    }
}