using Godot;

public sealed class Holder : Spatial
{
    public RigidBody Holding { get; set; }
    public bool Smooth { get; set; }
    private float _timer;
    private const float _forceTime = 0.01f;

    public override void _Ready()
    {
        Smooth = false;
    }

    public override void _Process(float delta)
    {
        if (Holding == null) return;

        if (Smooth)
        {
            _timer += delta;

            if (_timer > _forceTime)
            {
                _timer = 0f;
                HoldSmooth();
            }
        }
        else
        {
            HoldFixed();
        }

    }

    private void HoldFixed()
    {
        if (Holding == null) return;

        Holding.GlobalTransform = GlobalTransform;
    }

    private void HoldSmooth()
    {
        if (Holding == null) return;

        var desiredPos = GlobalTransform.origin;
        var currentPos = Holding.GlobalTransform.origin;

        Holding.ApplyCentralImpulse((desiredPos - currentPos) * 2f * 0.1f);
    }
}
