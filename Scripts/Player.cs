using Godot;

public sealed class Player : KinematicBody
{
    private const float _speed = 10f;
    private const float _mouseSensitivity = 0.005f;
    private Spatial _cameraPivot;

    public override void _Ready()
    {
        _cameraPivot = GetNode<Spatial>("CameraPivot");
    }

    public override void _Process(float delta)
    {
        var dir = Vector3.Zero;

        if (Input.IsActionPressed("forward")) dir.z += 1;
        if (Input.IsActionPressed("back")) dir.z += -1;
        if (Input.IsActionPressed("right")) dir.x += 1;
        if (Input.IsActionPressed("left")) dir.x -= 1;

        MoveAndSlide(dir.Normalized() * _speed, Vector3.Up);
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
    }
}
