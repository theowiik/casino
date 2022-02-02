using Godot;

public sealed class BallSpawner : Node
{
    private Position3D _spawnerPosition;
    private PackedScene _ballScene;
    private PackedScene _chip;

    public override void _Ready()
    {
        _ballScene = ResourceLoader.Load<PackedScene>("res://Prefabs/Ball.tscn");
        _chip = ResourceLoader.Load<PackedScene>("res://Prefabs/PokerChip.tscn");

        _spawnerPosition = GetNode<Position3D>("Tube/SpawnerPosition");
    }

    private void OnButtonPressed()
    {
        for (int i = 0; i < 10; i++)
        {
            Spatial spatial;

            if (i % 2 == 0)
            {
                spatial = _ballScene.Instance() as Spatial;
            }
            else
            {
                var chip = _chip.Instance() as PokerChip;
                chip.Money = 100;
                spatial = chip;
            }

            // TODO: Spawn as child to root node
            AddChild(spatial);
            var globalSpawnPos = _spawnerPosition.GlobalTransform.origin;
            spatial.GlobalTransform = new Transform(Quat.Identity, globalSpawnPos);
        }
    }
}
