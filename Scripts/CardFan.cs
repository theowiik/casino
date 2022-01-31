using Godot;
using System.Collections.Generic;

public sealed class CardFan : Spatial
{
    private IList<PhysicalCard> _cards;
    private PackedScene _cardScene;
    private const float Radius = 3f;
    private const float PercentageOfCircle = 0.5f;
    private const float ZOffset = 0.1f;

    public override void _Ready()
    {
        _cardScene = ResourceLoader.Load<PackedScene>("res://Prefabs/Cards/PhysicalCard.tscn");
        _cards = new List<PhysicalCard>();

        for (int i = 0; i < 10; i++)
            Add(new Card(21, "test card", Suit.Clubs));
    }

    public override void _PhysicsProcess(float delta)
    {
        Rotate(Vector3.Up, delta * Mathf.Pi / 2);

        if (Input.IsActionJustPressed("ui_right"))
        {
            Add(new Card(21, "during dev", Suit.Clubs));
        }
    }

    private void Place()
    {
        var i = 0;
        var angleBetweenCardsRad = PercentageOfCircle * Mathf.Tau / _cards.Count;
        var totalRad = angleBetweenCardsRad * _cards.Count;
        var baseVector = new Vector3(0, Radius, 0);
        var center = Transform.origin;

        foreach (var card in _cards)
        {
            var zBasis = card.Transform.basis.z.Normalized();
            var angle = i * angleBetweenCardsRad + angleBetweenCardsRad / 2 - Mathf.Pi / 2;
            card.Rotation = Vector3.Zero;

            card.SetLocalPosition(baseVector.Rotated(zBasis, angle) + new Vector3(0, 0, i * ZOffset));
            card.Rotate(zBasis, angle);

            i++;
        }
    }

    public void Add(IEnumerable<Card> cards)
    {
        foreach (var card in cards)
            Add(card);
    }

    public void Add(Card cardModel)
    {
        var card = _cardScene.Instance<PhysicalCard>();
        card.SetAsStatic();
        AddChild(card);
        card.Text = cardModel.Name;
        _cards.Add(card);

        Place();
    }

    public void Clear()
    {
        foreach (var card in _cards)
            card.QueueFree();

        _cards.Clear();
    }
}
