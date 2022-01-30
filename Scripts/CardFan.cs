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

        Place();
    }

    private void Place()
    {
        var i = 0;
        var angleBetweenCardsRad = PercentageOfCircle * Mathf.Tau / _cards.Count;
        var totalRad = angleBetweenCardsRad * _cards.Count;
        var baseVector = new Vector3(0, Radius, 0);

        foreach (var card in _cards)
        {
            var rotated = baseVector.Rotated(Vector3.Forward, i * angleBetweenCardsRad);
            card.Transform = new Transform(card.Transform.basis, rotated + new Vector3(0, 0, ZOffset));
            card.RotateZ(-i * angleBetweenCardsRad);
            i++;
        }
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
