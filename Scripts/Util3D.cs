using Godot;

// TODO: Add namespaces
public static class Util3D
{
    public static void SetPosition(this PhysicalCard spatial, Vector3 localPosition)
    {
        spatial.Transform = new Transform(spatial.Transform.basis, localPosition);
    }
}
