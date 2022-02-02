using Godot;

public sealed class PlayerBetButton : Button, IInteractable
{
    [Signal]
    public delegate void PlayerPressed(Player who, float amount);

    public override void Interact(Player interactedBy)
    {
        Press(interactedBy, 10);
    }

    private void Press(Player who, float amount)
    {
        if (!Clickable) return;

        AnimateButton();
        EmitSignal(nameof(PlayerPressed), who, amount);
    }

}
