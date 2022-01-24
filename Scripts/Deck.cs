using System.Collections.Generic;
using System;

public sealed class Deck
{
    private IList<Card> _cards { get; set; }

    public Deck()
    {
        _cards = new List<Card>();
    }

    public Deck(IEnumerable<Card> cards)
    {
        _cards = new List<Card>(cards);
    }

    public Card TakeTopCard()
    {
        if (_cards.Count == 0)
        {
            throw new Exception("Deck is empty");
        }

        var card = _cards[0];
        _cards.RemoveAt(0);
        return card;
    }

    public static IList<Card> BuildPokerDeck()
    {
        var deck = new List<Card>();
        var suits = new Suit[] { Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades };
        var values = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        foreach (var suit in suits)
        {
            foreach (var value in values)
            {
                var name = $"{value} of {suit}";
                deck.Add(new Card(value, name, suit));
            }
        }

        var royalties = new string[] { "Jack", "Queen", "King", "Ace" }; // TODO: Ace is 1 and 10.
        foreach (var royalty in royalties)
        {
            foreach (var suit in suits)
            {
                var name = $"{royalty} of {suit}";
                deck.Add(new Card(10, name, suit));
            }
        }

        return deck;
    }

    public void Shuffle()
    {
        var current = new List<Card>(_cards);
        var shuffledDeck = new List<Card>();

        var rnd = new Random();
        while (current.Count > 0)
        {
            var index = rnd.Next(0, current.Count);
            shuffledDeck.Add(current[index]);
            current.RemoveAt(index);
        }

        _cards = shuffledDeck;
    }
}
