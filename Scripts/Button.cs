using Godot;

public sealed class Button : Node
{
    [Signal]
    public delegate void ButtonPressed();
    private AnimationPlayer _animationPlayer;
    private bool _clickable;

    public bool Clickable
    {
        get
        {
            return _clickable;
        }
        set
        {
            _clickable = value;

            if (_clickable)
                _animationPlayer.Play("clickable");
            else
                _animationPlayer.Play("disabled");
        }
    }

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        Clickable = true;
    }

    private void AnimateButton()
    {
        _animationPlayer.Stop();
        _animationPlayer.Play("press");
    }

    public void Press(Player who)
    {
        GD.Print("Button pressed by " + who.PlayerName);
        AnimateButton();
        EmitSignal(nameof(ButtonPressed));
    }
}
