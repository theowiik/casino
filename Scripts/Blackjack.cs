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

    public override void _Ready()
    {
        _hitButton = GetNode<Button>("Hit");
        _standButton = GetNode<Button>("Stand");
        _playerJoinButton = GetNode<PlayerBetButton>("PlayerJoinButton");
        _playerJoinButton.Connect(nameof(PlayerBetButton.PlayerPressed), this, nameof(OnPlayerJoin));

        _hitButton.Clickable = false;
        _standButton.Clickable = false;

        _state = BlackjackState.TakingBets;
        _blackjack = new BlackjackModel();
        DisplayScore();
    }

    private void OnPlayerJoin(Player player, int bet)
    {
        player.TakeMoney(bet);

        _blackjack.StartGame();
        DisplayScore();
        _hitButton.Clickable = true;
        _standButton.Clickable = true;
        _state = BlackjackState.PlayerTurn;
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
