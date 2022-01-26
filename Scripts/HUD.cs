using Godot;

public sealed class HUD : Control
{
    private Label _moneyLabel;

    public override void _Ready()
    {
        _moneyLabel = GetNode<Label>("MoneyLabel");
    }

    public void OnMoneyChanged(int money)
    {
        _moneyLabel.Text = $"Money: {money}";
    }
}
