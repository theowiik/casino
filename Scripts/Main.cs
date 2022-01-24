using Godot;

public sealed class Main : Node
{
    public override void _Ready()
    {
        Input.SetMouseMode(Input.MouseMode.Captured);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel")) {
            GetTree().Quit();
        }
    }
}
