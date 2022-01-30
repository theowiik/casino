using Godot;
using System.Collections.Generic;

public sealed class CardFan : Spatial
{
    private IList<PhysicalCard> _cards;
    private PackedScene _cardScene;
    private const float Radius = 1f;
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
        var basis = Transform.basis;
        var angleBetweenCardsRad = PercentageOfCircle * Mathf.Tau / _cards.Count;
        var totalRad = angleBetweenCardsRad * _cards.Count;
        var baseVector = basis.y.Normalized() * Radius;

        foreach (var card in _cards)
        {
            var forward = basis.z.Normalized();
            var rotated = baseVector.Rotated(forward, i * angleBetweenCardsRad);
            card.Transform = new Transform(basis, rotated + basis.z.Normalized() * ZOffset);
            //card.RotateZ(-i * angleBetweenCardsRad);
            i++;
        }
    }

    public void Add(Card card)
    {
        BuildAndAdd(card);
        Place();
    }

    public void Clear()
    {
        foreach (var card in _cards)
            card.QueueFree();

        _cards.Clear();
    }

    public override void _PhysicsProcess(float delta)
    {
        RotateY(delta * Mathf.Tau / 10);

        if (Input.IsActionJustPressed("ui_right"))
        {
            Clear();
            for (int i = 0; i < 1; i++)
            {
                Add(new Card(21, "test card", Suit.Clubs));
            }
        }
    }

    private void BuildAndAdd(Card cardModel)
    {
        var card = _cardScene.Instance<PhysicalCard>();
        card.SetAsStatic();
        AddChild(card);
        card.Text = cardModel.Name;
        _cards.Add(card);
    }
}
