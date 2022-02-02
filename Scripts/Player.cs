using Godot;

public sealed class Player : KinematicBody
{
    [Signal]
    public delegate void MoneyChanged(int money);

    public enum PlayerState
    {
        Walking,
        Sitting
    }

    public PlayerState State { get; set; }
    private int _money;
    public string PlayerName { get; } = "Jeff";
    private Spatial _cameraPivot;
    private RayWrapper _interactRay;
    private Vector3 _velocity = Vector3.Zero;
    private Holder _holder;
    private const float speed = 15f;
    private const float MouseSensitivity = 0.005f;
    private const float Gravity = 50f;
    private const float JumpForce = 20f;

    private int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            EmitSignal(nameof(MoneyChanged), _money);
        }
    }

    public RayWrapper VisionWrapper => _interactRay;

    public override void _Ready()
    {
        State = PlayerState.Walking;
        _holder = GetNode<Holder>("Holder");
        _cameraPivot = GetNode<Spatial>("CameraPivot");
        _interactRay = GetNode<RayWrapper>("CameraPivot/Camera/InteractRay");

        _interactRay.Connect(nameof(RayWrapper.HoverStarted), this, nameof(OnCollisionEnter));
        _interactRay.Connect(nameof(RayWrapper.HoverEnded), this, nameof(OnCollisionExit));

        Money = 100;
    }

    public int TakeMoney(int amount)
    {
        // TODO: Make money threadsafe
        if (amount <= 0) return 0;
        if (Money < amount) throw new System.Exception("Not enough money");

        Money -= amount;
        return amount;
    }

    public void GiveMoney(int amount)
    {
        if (amount <= 0) return;
        Money += amount;
    }

    public override void _PhysicsProcess(float delta)
    {
        if (State == PlayerState.Walking)
        {
            Move(delta);
        }
    }

    private void Move(float delta)
    {
        // Walking
        var desiredVelocity = GetInputVector() * speed;

        // Gravity
        _velocity = new Vector3(desiredVelocity.x, _velocity.y - Gravity * delta, desiredVelocity.z);

        // Jump
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
            _velocity += new Vector3(0, JumpForce, 0);

        _velocity = MoveAndSlide(_velocity, Vector3.Up);
    }

    private Vector3 GetInputVector()
    {
        var direction = Vector3.Zero;
        var basis = GlobalTransform.basis;
        var z = basis.z;
        var x = basis.x;

        if (Input.IsActionPressed("forward")) direction -= z;
        if (Input.IsActionPressed("back")) direction += z;
        if (Input.IsActionPressed("right")) direction += x;
        if (Input.IsActionPressed("left")) direction -= x;

        return direction.Normalized();
    }

    private void Interact()
    {
        var result = _interactRay.GetCollider() as Node;
        if (result == null) return;

        if (result.IsInGroup("holdable") && result is RigidBody rb)
        {
            _holder.Holding = rb;
            return;
        }

        if (result is IInteractable interactable)
            interactable.Interact(this);
    }

    private void ExitChair()
    {
        State = PlayerState.Walking;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion e)
        {
            // Todo: does not take into account delta time.
            RotateY(-e.Relative.x * MouseSensitivity);
            _cameraPivot.RotateX(-e.Relative.y * MouseSensitivity);

            _cameraPivot.Rotation = new Vector3(
                Mathf.Clamp(_cameraPivot.Rotation.x, -1.4f, 1.4f),
                _cameraPivot.Rotation.y,
                _cameraPivot.Rotation.z
            );

            GetTree().SetInputAsHandled();
            return;
        }

        if (@event.IsActionPressed("interact"))
        {
            Interact();
            GetTree().SetInputAsHandled();
            return;
        }

        if (@event.IsActionReleased("exit"))
        {
            ExitChair();
            GetTree().SetInputAsHandled();
            return;
        }
    }

    private void OnCollisionEnter(Node node)
    {
        if (node is IHoverable hoverable)
        {
            hoverable.HoverStarted();
        }
    }

    private void OnCollisionExit(Node node)
    {
        if (node is IHoverable hoverable)
        {
            hoverable.HoverEnded();
        }
    }
}
