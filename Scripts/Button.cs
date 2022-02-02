using Godot;

public class Button : Node, IInteractable, IHoverHintable
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

    protected void AnimateButton()
    {
        _animationPlayer.Stop();
        _animationPlayer.Play("press");
    }

    private void Press()
    {
        if (!Clickable) return;

        AnimateButton();
        EmitSignal(nameof(ButtonPressed));
    }

    public virtual void Interact(Player interactedBy)
    {
        Press();
    }

    public Hint GetHint()
    {
        return new Hint("E", "clicky :)");
    }
}
