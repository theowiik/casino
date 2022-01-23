using Godot;

public sealed class Player : KinematicBody
{
    private const float _speed = 10f;
    private const float _mouseSensitivity = 0.005f;
    private Spatial _cameraPivot;
    private RayCast _interactRay;

    public override void _Ready()
    {
        _cameraPivot = GetNode<Spatial>("CameraPivot");
        _interactRay = GetNode<RayCast>("CameraPivot/Camera/InteractRay");
    }

    public override void _Process(float delta)
    {
        Move();
    }

    private void Move()
    {
        var dir = Vector3.Zero;
        var basis = GlobalTransform.basis;
        var z = basis.z;
        var x = basis.x;

        if (Input.IsActionPressed("forward")) dir += z;
        if (Input.IsActionPressed("back")) dir -= z;
        if (Input.IsActionPressed("right")) dir -= x;
        if (Input.IsActionPressed("left")) dir += x;

        MoveAndSlide(dir * _speed, Vector3.Up);
    }

    private void Interact()
    {
        GD.Print(_interactRay.IsColliding());
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion e) {
            // Todo: does not take into account delta time.
            RotateY(-e.Relative.x * _mouseSensitivity);
            _cameraPivot.RotateX(e.Relative.y * _mouseSensitivity);

            _cameraPivot.Rotation = new Vector3(
                
                Mathf.Clamp(_cameraPivot.Rotation.x, -1.2f, 1.2f),
                _cameraPivot.Rotation.y,
                _cameraPivot.Rotation.z
            );
        }

        if (@event.IsActionPressed("interact")) {
            Interact();
        }
    }
}
