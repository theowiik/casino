using Godot;

public sealed class Player : KinematicBody
{
    private enum State {
        Walking,
        Sitting
    }

    public string PlayerName { get; } = "Jeff";
    private int _money;
    private const float _speed = 15f;
    private const float _mouseSensitivity = 0.005f;
    private Spatial _cameraPivot;
    private RayCast _interactRay;
    private State _state = State.Walking;
    private Vector3 _velocity = Vector3.Zero;
    private float _gravity = 30f;
    private float _jumpForce = 20f;

    public override void _Ready()
    {
        _money = 100;
        _cameraPivot = GetNode<Spatial>("CameraPivot");
        _interactRay = GetNode<RayCast>("CameraPivot/Camera/InteractRay");
    }

    public override void _PhysicsProcess(float delta)
    {
        if (_state == State.Walking) {
            Move(delta);
        }
    }

    private void Move(float delta)
    {
        // Walking
        var desiredVelocity = GetInputVector() * _speed;

        // Gravity
        _velocity = new Vector3(desiredVelocity.x, _velocity.y - _gravity * delta, desiredVelocity.z);

        // Jump
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
            _velocity += new Vector3(0, _jumpForce, 0);

        _velocity = MoveAndSlide(_velocity, Vector3.Up);
    }

    private Vector3 GetInputVector()
    {
        var direction = Vector3.Zero;
        var basis = GlobalTransform.basis;
        var z = basis.z;
        var x = basis.x;

        if (Input.IsActionPressed("forward")) direction += z;
        if (Input.IsActionPressed("back")) direction -= z;
        if (Input.IsActionPressed("right")) direction -= x;
        if (Input.IsActionPressed("left")) direction += x;

        return direction.Normalized();
    }

    private void Interact()
    {
        GD.Print("Interact");
        var result = _interactRay.GetCollider();

        if (result is Chair chair) {
            _state = State.Sitting;
            var chairPos = chair.GetGlobalSitPosition();
            GlobalTransform = new Transform(Quat.Identity, chairPos);
        } else if (result is Button button) {
            button.Press(this);
        }
    }

    private void ExitChair()
    {
        _state = State.Walking;
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

        if (@event.IsActionReleased("exit")) {
            ExitChair();
        }
    }
}
