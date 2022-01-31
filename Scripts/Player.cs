using Godot;

public sealed class Player : KinematicBody
{
    [Signal]
    public delegate void MoneyChanged(int money);

    private enum State
    {
        Walking,
        Sitting
    }

    private int _money;
    public string PlayerName { get; } = "Jeff";
    private const float _speed = 15f;
    private const float _mouseSensitivity = 0.005f;
    private Spatial _cameraPivot;
    private RayCast _interactRay;
    private State _state = State.Walking;
    private Vector3 _velocity = Vector3.Zero;
    private float _gravity = 50f;
    private float _jumpForce = 20f;
    private HUD _hud;

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

    public override void _Ready()
    {
        _cameraPivot = GetNode<Spatial>("CameraPivot");
        _interactRay = GetNode<RayCast>("CameraPivot/Camera/InteractRay");
        _hud = GetNode<HUD>("HUD");

        this.Connect(nameof(MoneyChanged), _hud, nameof(HUD.OnMoneyChanged));

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
        if (_state == State.Walking)
        {
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
        var result = _interactRay.GetCollider();

        switch (result)
        {
            case Chair chair:
                _state = State.Sitting;
                var chairPos = chair.GetGlobalSitPosition();
                GlobalTransform = new Transform(Quat.Identity, chairPos);
                break;
            case PlayerBetButton betButton:
                betButton.Press(this, 10);
                break;
            case Button button:
                button.Press();
                break;
            case Jukebox jukebox:
                jukebox.PlayPause();
                break;
        }
    }

    private void ExitChair()
    {
        _state = State.Walking;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion e)
        {
            // Todo: does not take into account delta time.
            RotateY(-e.Relative.x * _mouseSensitivity);
            _cameraPivot.RotateX(e.Relative.y * _mouseSensitivity);

            _cameraPivot.Rotation = new Vector3(

                Mathf.Clamp(_cameraPivot.Rotation.x, -1.2f, 1.2f),
                _cameraPivot.Rotation.y,
                _cameraPivot.Rotation.z
            );
        }

        if (@event.IsActionPressed("interact"))
        {
            Interact();
        }

        if (@event.IsActionReleased("exit"))
        {
            ExitChair();
        }
    }
}
