using Godot;
using casino.Scripts.Core;

public sealed class PokerChip : RigidBody, IMoneyable, IInteractable
{
    private MoneyDelegate _moneyDelegate;

    public override void _Ready()
    {
        _moneyDelegate = new MoneyDelegate();
    }

    public int GetBalance() => _moneyDelegate.GetBalance();
    public void Give(int amount) => _moneyDelegate.Give(amount);
    public int Take(int amount) => _moneyDelegate.Take(amount);
    public int TakeAll() => _moneyDelegate.TakeAll();

    public void Interact(Player interactedBy)
    {
        interactedBy.Give(TakeAll());
        QueueFree();
    }
}
