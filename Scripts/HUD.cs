using Godot;

public sealed class HUD : Control
{
    private Label _moneyLabel;
    private Label _hoverHintLabel;
    private CPUParticles2D _moneyParticles;
    private int _lastMoney;
    private const float MoneyParticlesTimeSec = 0.4f;
    private Timer _moneyParticlesTimer;

    public override void _Ready()
    {
        _moneyLabel = GetNode<Label>("MoneyLabel");
        _hoverHintLabel = GetNode<Label>("HoverHintLabel");
        _moneyParticles = GetNode<CPUParticles2D>("MoneyParticles");
        _moneyParticlesTimer = GetNode<Timer>("MoneyParticlesTimer");

        _moneyParticlesTimer.Connect("timeout", this, nameof(OnMoneyParticlesTimeout));
    }

    public void OnMoneyChanged(int money)
    {
        _moneyLabel.Text = $"Money: {money}";

        if (_lastMoney < money && _lastMoney != 0)
        {
            _moneyParticles.Emitting = true;
            _moneyParticlesTimer.Start(MoneyParticlesTimeSec);
        }

        _lastMoney = money;
    }

    public void OnHoverHintChanged(Hint hint)
    {
        if (hint == null || !hint.Show)
            _hoverHintLabel.Text = "";
        else
            _hoverHintLabel.Text = $"{hint.Key} - {hint.Text}";
    }

    private void OnMoneyParticlesTimeout()
    {
        _moneyParticles.Emitting = false;
    }
}
