using Godot;

public sealed class Main : Node
{
    [Signal]
    public delegate void HintChanged(Hint hint);

    private HUD _hud;
    private Player _player;

    public override void _Ready()
    {
        _player = GetNode<Player>("Player");
        _hud = GetNode<HUD>("HUD");
        Connect(nameof(HintChanged), _hud, nameof(HUD.OnHoverHintChanged));
        _player.Connect(nameof(Player.MoneyChanged), _hud, nameof(HUD.OnMoneyChanged));
        _player.VisionWrapper.Connect(nameof(RayWrapper.HoverStarted), this, nameof(OnHoverStarted));
        _player.VisionWrapper.Connect(nameof(RayWrapper.HoverEnded), this, nameof(OnHoverEnded));

        Input.SetMouseMode(Input.MouseMode.Captured);
    }

    private void OnHoverStarted(Node node)
    {
        if (node is IHoverHintable hintable)
        {
            EmitSignal(nameof(HintChanged), hintable.GetHint());
        }
    }

    private void OnHoverEnded(Node node)
    {
        EmitSignal(nameof(HintChanged), Hint.Hidden);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel"))
        {
            GetTree().Quit();
        }
    }
}
