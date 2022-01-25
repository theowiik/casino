using Godot;

public sealed class BallSpawner : Node
{
    private Position3D _spawnerPosition;
    private PackedScene _ballScene;

    public override void _Ready()
    {
        _ballScene = ResourceLoader.Load<PackedScene>("res://Prefabs/Ball.tscn");
        _spawnerPosition = GetNode<Position3D>("Tube/SpawnerPosition");
    }

    private void OnButtonPressed()
    {
        for (int i = 0; i < 10; i++)
        {
            var ball = _ballScene.Instance<Spatial>();

            // TODO: Spawn as child to root node
            AddChild(ball);
            var globalSpawnPos = _spawnerPosition.GlobalTransform.origin;
            ball.GlobalTransform = new Transform(Quat.Identity, globalSpawnPos);
        }
    }
}
