using Godot;

// TODO: Add namespaces
public static class Util3D
{
    public static void SetLocalPosition(this Spatial spatial, Vector3 localPosition)
    {
        spatial.Transform = new Transform(spatial.Transform.basis, localPosition);
    }

    public static void SetGlobalPosition(this Spatial spatial, Vector3 globalPosition)
    {
        spatial.GlobalTransform = new Transform(spatial.Transform.basis, globalPosition);
    }
}
