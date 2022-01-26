using Godot;

public sealed class PlayerBetButton : Button
{
    [Signal]
    public delegate void PlayerPressed(Player who, float amount);

    public void Press(Player who, float amount)
    {
        if (!Clickable) return;

        GD.Print("special button");
        AnimateButton();
        EmitSignal(nameof(PlayerPressed), who, amount);
    }
}
