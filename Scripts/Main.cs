using Godot;

public sealed class Main : Node
{
    private Camera _playerCamera;
    private RigidBody _pickup;
    private float _timer;
    private const float _forceTime = 0.01f;

    public override void _Ready()
    {
        Input.SetMouseMode(Input.MouseMode.Captured);
        
        _pickup = GetNode<RigidBody>("Holding");
        _playerCamera = GetNode<Camera>("Player/CameraPivot/Camera");

        // Label demo
        var viewport = GetNode<Viewport>("Wasd/Viewport");
        var sprite3d = GetNode<Sprite3D>("Wasd");
        sprite3d.Texture = viewport.GetTexture();
    }

    public override void _Process(float delta)
    {
        _timer += delta;

        if (_timer > _forceTime)
        {
            _timer = 0f;
            // Hold();
        }
    }

    public override void _PhysicsProcess(float delta)
    {
    }

    private void Hold()
    {
        var desiredPos = _playerCamera.GlobalTransform.origin - _playerCamera.GlobalTransform.basis.z * 5;
        var currentPos = _pickup.GlobalTransform.origin;

        _pickup.ApplyCentralImpulse((desiredPos - currentPos) * 2f * 0.1f);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel"))
        {
            GetTree().Quit();
        }
    }

    private Camera GetCameraViewport()
    {
        var camera = GetNode<Camera>("Player/CameraPivot/Camera");
        return camera.GetViewport().GetCamera();
    }
}
