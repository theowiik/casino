using Godot;

public sealed class HUD : Control
{
    private Label _moneyLabel;
    private Label _hoverHintLabel;

    public override void _Ready()
    {
        _moneyLabel = GetNode<Label>("MoneyLabel");
        _hoverHintLabel = GetNode<Label>("HoverHintLabel");
    }

    public void OnMoneyChanged(int money)
    {
        _moneyLabel.Text = $"Money: {money}";
    }

    public void OnHoverHintChanged(Hint hint)
    {
        if (hint == null || !hint.Show)
            _hoverHintLabel.Text = "";
        else
            _hoverHintLabel.Text = $"{hint.Key} - {hint.Text}";
    }
}
