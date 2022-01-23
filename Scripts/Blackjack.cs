using Godot;
using System.Collections.Generic;

public sealed class Blackjack : Node
{
    private enum Suit {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }

    private struct Card
    {
        public int Value;
        public string Name;
        public Suit Suit;

        public Card(int value, string name, Suit suit)
        {
            Value = value;
            Name = name;
            Suit = suit;
        }
    }

    private IList<Card> BuildDeck() {
        var deck = new List<Card>();
        var suits = new Suit[] { Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades };
        var values = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        foreach (var suit in suits) {
            foreach (var value in values) {
                var name = $"{value} of {suit}";
                deck.Add(new Card { Value = value, Name = name, Suit = suit });
            }
        }

        var royalties = new string[] { "Jack", "Queen", "King", "Ace" }; // TODO: Ace is 1 and 10.
        foreach (var royalty in royalties) {
            foreach (var suit in suits) {
                var name = $"{royalty} of {suit}";
                deck.Add(new Card { Value = 10, Name = name, Suit = suit });
            }
        }

        return deck;
    }

    private IList<Card> BuildShuffledDeck() {
        var deck = BuildDeck();
        var shuffledDeck = new List<Card>();

        var random = new RandomNumberGenerator();
        while (deck.Count > 0) {
            var index = random.RandiRange(0, deck.Count);
            shuffledDeck.Add(deck[index]);
            deck.RemoveAt(index);
        }

        return shuffledDeck;
    }
}

