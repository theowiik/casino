using Godot;

public sealed class Chair : Spatial
{
    public Vector3 GetGlobalSitPosition() {
        return GetNode<Spatial>("SitPosition").GlobalTransform.origin;
    }
}
