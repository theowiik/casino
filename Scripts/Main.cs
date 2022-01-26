using Godot;

public sealed class Main : Node
{
    private Camera _playerCamera;
    private RigidBody _pickup;

    public override void _Ready()
    {
        _pickup = GetNode<RigidBody>("Holding");
        _playerCamera = GetNode<Camera>("Player/CameraPivot/Camera");
        Input.SetMouseMode(Input.MouseMode.Captured);
    }

    public override void _PhysicsProcess(float delta)
    {
        var desiredPos = _playerCamera.GlobalTransform.origin - _playerCamera.GlobalTransform.basis.z * 5;
        var currentPos = _pickup.GlobalTransform.origin;

        _pickup.ApplyCentralImpulse(desiredPos - currentPos);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel")) {
            GetTree().Quit();
        }
    }
}
