using Godot;

public sealed class Button : Node
{
    [Signal]
    public delegate void ButtonPressed();

    public void Press()
    {
        GD.Print("Button pressed");
    }
}
