using System.Collections.Generic;

public sealed class BlackjackModel
{
    public IEnumerable<Card> PlayerCards => _playerHand;
    public IEnumerable<Card> DealerCards => _dealerHand;

    private Deck _deck;
    private IList<Card> _dealerHand;
    private IList<Card> _playerHand;

    public BlackjackModel()
    {
        _deck = new Deck();
        _dealerHand = new List<Card>();
        _playerHand = new List<Card>();
    }

    public void StartGame()
    {
        _dealerHand.Clear();
        _playerHand.Clear();

        _deck = new Deck(Deck.BuildPokerDeck());
        _deck.Shuffle();

        _dealerHand.Add(_deck.TakeTopCard());
        _playerHand.Add(_deck.TakeTopCard());
    }

    /// <summary>
    ///     Adds a card from the deck to the player's hand. Returns the card that was added.
    /// </summary>
    public Card Hit()
    {
        var card = _deck.TakeTopCard();
        _playerHand.Add(card);
        return card;
    }

    public void Stand()
    {
        while (GetDealerScore() < 17)
        {
            _dealerHand.Add(_deck.TakeTopCard());
        }
    }

    public int GetDealerScore() {
        return GetScore(_dealerHand);
    }

    public int GetPlayerScore()
    {
        return GetScore(_playerHand);
    }

    private int GetScore(IEnumerable<Card> cards)
    {
        var score = 0;
        foreach (var card in cards)
        {
            score += card.Value;
        }
        return score;
    }

    public bool IsPlayerBust()
    {
        return GetPlayerScore() > 21;
    }

    public bool IsDealerBust()
    {
        return GetDealerScore() > 21;
    }
}