using Godot;

public sealed class CubeHover : RigidBody
{
    public override void _Ready()
    {
        Connect("body_entered", this, nameof(OnMouseEntered));
    }

    public void OnMouseEntered(Node node)
    {
        GD.Print("Mouse entered");
    }

    private void _on_CubeHover_body_shape_entered(RID body_rid, object body, int body_shape_index, int local_shape_index)
    {
        GD.Print("test");// Replace with function body.
    }

}


