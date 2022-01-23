using Godot;

public sealed class Player : Spatial
{
    public override void _Ready()
    {
        GD.Print("Hello World");
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionPressed("Forward")) {
            
        }
    }
}
