using Godot;

public sealed class Blackjack : Node
{
    private enum BlackjackState
    {
        PlayerTurn,
        DealerTurn,
        GameOver
    }

    private BlackjackModel _blackjack;
    private BlackjackState _state;

    public override void _Ready()
    {
        _state = BlackjackState.PlayerTurn;
        _blackjack = new BlackjackModel();
        _blackjack.StartGame();
        DisplayScore();
    }

    private void OnHitPressed()
    {
        GD.Print("Hit pressed");
        if (_state != BlackjackState.PlayerTurn) return;

        _blackjack.Hit();
        DisplayScore();

        if (_blackjack.IsPlayerBust())
        {
            GD.Print("Player bust");
            _state = BlackjackState.GameOver;
        }
    }

    private void OnStandPressed()
    {
        GD.Print("Stand pressed");
        if (_state != BlackjackState.PlayerTurn) return;

        _state = BlackjackState.DealerTurn;
        _blackjack.Stand();

        DisplayScore();

        if (_blackjack.IsDealerBust())
        {
            GD.Print("Dealer bust");
            _state = BlackjackState.GameOver;
        }
        else if (_blackjack.GetDealerScore() > _blackjack.GetPlayerScore())
        {
            GD.Print("Dealer wins");
            _state = BlackjackState.GameOver;
        }
        else if (_blackjack.GetDealerScore() == _blackjack.GetPlayerScore())
        {
            GD.Print("Tie");
            _state = BlackjackState.GameOver;
        }
        else
        {
            GD.Print("Player wins");
            _state = BlackjackState.GameOver;
        }
    }

    private void DisplayScore()
    {
        GD.Print("------------");
        GD.Print("Dealer score: " + _blackjack.GetDealerScore());
        GD.Print("Player score: " + _blackjack.GetPlayerScore());
    }
}
