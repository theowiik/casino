using Godot;

public sealed class Main : Node
{
    private Camera _playerCamera;
    private RigidBody _pickup;
    private float _timer;
    private const float _forceTime = 0.01f;
    // private Label _label;

    public override void _Ready()
    {
        // _label = GetNode<Label>("Sprite3D/Label");
        _pickup = GetNode<RigidBody>("Holding");
        _playerCamera = GetNode<Camera>("Player/CameraPivot/Camera");
        Input.SetMouseMode(Input.MouseMode.Captured);

        // PositionLabel();

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

    // private void PositionLabel()
    // {
    //     var viewport = GetCameraViewport();
    //     var camPos = viewport.GlobalTransform.origin;
    //     var offset = new Vector2(_label.RectSize.x / 2, 0);
    //     _label.RectPosition = viewport.UnprojectPosition(new Vector3(0, 3, 0)) - offset;
    // }

    // onready var sprite = $Sprite3D
    // onready var label = $Sprite3D/Label

    // func get_camera():
    //     var r = get_node('/root')
    //     return r.get_viewport().get_camera()

    // func position_label(label:Label, point3D:Vector3):
    //     var camera = get_camera()
    //     var cam_pos = camera.translation
    //     var offset = Vector2(label.get_size().x/2, 0)
    //     label.rect_position = camera.unproject_position(point3D) - offset
}
