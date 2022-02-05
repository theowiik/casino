using Godot;

public sealed class Chair : Spatial, IInteractable, IHoverHintable
{
    public void Interact(Player interactedBy)
    {
        var globalSitPos = GetNode<Spatial>("SitPosition").GlobalTransform.origin;
        interactedBy.SetGlobalPosition(globalSitPos);
        interactedBy.State = Player.PlayerState.Sitting;
    }

    public Hint GetHint()
    {
        return new Hint("E", "Sit");
    }
}
