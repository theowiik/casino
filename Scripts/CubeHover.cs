using Godot;

public sealed class CubeHover : RigidBody
{
    public override void _Ready()
    {
        var area = GetNode<Area>("Area");
        area.Connect("area_entered", this, nameof(OnAreaEntered));
    }

    private void OnAreaEntered(Area area)
    {
        GD.Print("Entered area");
    }
}
