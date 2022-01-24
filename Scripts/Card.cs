/// <summary>
///   Immutable card.
/// </summary>
public struct Card
{
    public int Value { get; }
    public string Name { get; }
    public Suit Suit { get; }

    public Card(int value, string name, Suit suit)
    {
        Value = value;
        Name = name;
        Suit = suit;
    }
}