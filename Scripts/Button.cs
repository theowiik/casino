using Godot;

public class Button : Node, IInteractable, IHoverHintable
{
    [Signal]
    public delegate void ButtonPressed();
    private AnimationPlayer _animationPlayer;
    private bool _clickable;
    private Hint _hint;

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
        _hint = new Hint("E", "Press");
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
        if (Clickable)
            return _hint;
        else
            return new Hint("Disabled");
    }

    public void SetHint(Hint hint)
    {
        _hint = hint;
    }
}
