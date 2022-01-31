using Godot;

public sealed class Chair : Spatial, IInteractable
{
    public void Interact(Player interactedBy)
    {
        var globalSitPos = GetNode<Spatial>("SitPosition").GlobalTransform.origin;
        interactedBy.SetGlobalPosition(globalSitPos);
        interactedBy.State = Player.PlayerState.Sitting;
    }
}
