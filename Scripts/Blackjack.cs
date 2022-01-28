using Godot;

public sealed class Blackjack : Node
{
    private enum BlackjackState
    {
        TakingBets,
        PlayerTurn,
        DealerTurn,
        GameOver
    }

    private BlackjackModel _blackjack;
    private BlackjackState _state;
    private Button _hitButton;
    private Button _standButton;
    private PlayerBetButton _playerJoinButton;
    private Player _who;
    private int _bet;

    public override void _Ready()
    {
        _hitButton = GetNode<Button>("Hit");
        _standButton = GetNode<Button>("Stand");
        _playerJoinButton = GetNode<PlayerBetButton>("PlayerJoinButton");
        _playerJoinButton.Connect(nameof(PlayerBetButton.PlayerPressed), this, nameof(OnPlayerJoin));

        _state = BlackjackState.TakingBets;
        _blackjack = new BlackjackModel();

        Reset();
        DisplayScore();
    }

    private void OnPlayerJoin(Player player, int bet)
    {
        if (player is null || bet <= 0) return;

        _who = player;
        _bet = bet;

        player.TakeMoney(bet);

        _blackjack.StartGame();
        DisplayScore();
        _state = BlackjackState.PlayerTurn;

        SetButtonClickable(_state);
    }

    private void OnHitPressed()
    {
        if (_state != BlackjackState.PlayerTurn) return;

        _blackjack.Hit();
        DisplayScore();

        if (_blackjack.IsPlayerBust()) Reset();
    }

    private void OnStandPressed()
    {
        GD.Print("Stand pressed");
        if (_state != BlackjackState.PlayerTurn) return;

        _state = BlackjackState.DealerTurn;
        _blackjack.Stand();

        DisplayScore();

        if (_blackjack.IsDealerBust() && _blackjack.IsPlayerBust())
        {
            GD.Print("Both bust");
            Reset();
        }
        else if (_blackjack.GetDealerScore() > _blackjack.GetPlayerScore())
        {
            GD.Print("Dealer wins");
            Reset();
        }
        else if (_blackjack.GetDealerScore() == _blackjack.GetPlayerScore())
        {
            GD.Print("Tie, what happens here");
            Reset();
        }
        else
        {
            GD.Print("Player wins");
            _who.GiveMoney(_bet);
            Reset();
        }
    }

    private void Reset()
    {
        _state = BlackjackState.TakingBets;
        SetButtonClickable(_state);
    }

    private void SetButtonClickable(BlackjackState state)
    {
        switch (state)
        {
            case BlackjackState.TakingBets:
                _playerJoinButton.Clickable = true;
                _hitButton.Clickable = false;
                _standButton.Clickable = false;
                break;
            case BlackjackState.PlayerTurn:
                _playerJoinButton.Clickable = false;
                _hitButton.Clickable = true;
                _standButton.Clickable = true;
                break;
            case BlackjackState.DealerTurn:
                _playerJoinButton.Clickable = false;
                _hitButton.Clickable = false;
                _standButton.Clickable = false;
                break;
            case BlackjackState.GameOver:
                _playerJoinButton.Clickable = true;
                _hitButton.Clickable = false;
                _standButton.Clickable = false;
                break;
        }
    }

    private void DisplayScore()
    {
        GD.Print("------------");
        GD.Print("Dealer score: " + _blackjack.GetDealerScore());
        GD.Print("Player score: " + _blackjack.GetPlayerScore());
    }
}
