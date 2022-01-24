using Godot;

public sealed class Button : Node
{
    [Signal]
    public delegate void ButtonPressed();
    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
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
