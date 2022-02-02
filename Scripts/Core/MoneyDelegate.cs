using System;

namespace casino.Scripts.Core
{
    public sealed class MoneyDelegate : IMoneyable
    {
        private int _money;

        public MoneyDelegate(int money = 0)
        {
            _money = Math.Max(0, money);
        }

        public int GetBalance()
        {
            return _money;
        }

        public void Give(int amount)
        {
            if (amount <= 0) new ArgumentOutOfRangeException(nameof(amount), "must be positive");
            _money += amount;
        }

        public int Take(int amount)
        {
            // TODO: Make money threadsafe
            if (amount <= 0) return 0;
            if (_money < amount) throw new System.Exception("Not enough money");

            _money -= amount;
            return amount;
        }

        public int TakeAll()
        {
            var amount = _money;
            _money = 0;
            return amount;
        }
    }
}