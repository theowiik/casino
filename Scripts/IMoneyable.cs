public interface IMoneyable
{
    int GetBalance();

    // Out of range exception when amount is negative or zero
    void Give(int amount);

    // Out of range exception when amount greater than balance or negative
    int Take(int amount);

    int TakeAll();
}
