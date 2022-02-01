using Godot;

public sealed class RayWrapper : RayCast
{
    [Signal]
    public delegate void CollisionEnter(Node node);

    [Signal]
    public delegate void CollisionExit(Node node);

    public Object _lastCollide;
    private const float PollingRateSeconds = 0.1f;
    private float _pollingTimer;

    public override void _Process(float delta)
    {
        _pollingTimer += delta;

        if (_pollingTimer >= PollingRateSeconds)
        {
            _pollingTimer = 0;
            CheckCollision();
        }
    }

    private void CheckCollision()
    {
        var collider = GetCollider();

        // Nothing changed - nothing hovered
        if (collider == null && _lastCollide == null)
            return;

        // Nothing changed - still hovering
        if (collider != null && collider == _lastCollide)
            return;

        // Object left AND new one entered
        if (collider != null && collider != _lastCollide)
        {
            EmitSignal(nameof(CollisionExit), _lastCollide);
            _lastCollide = collider;
            EmitSignal(nameof(CollisionEnter), _lastCollide);
            return;
        }

        // Object left
        if (collider == null && _lastCollide != null)
        {
            EmitSignal(nameof(CollisionExit), _lastCollide);
            _lastCollide = null;
            return;
        }

        // Object entered
        if (collider != null)
        {
            EmitSignal(nameof(CollisionEnter), collider);
            _lastCollide = collider;
        }

        throw new System.Exception("RayWrapper: This should never happen");
    }
}
