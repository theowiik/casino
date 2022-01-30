using Godot;
using System.Collections.Generic;

public sealed class CardFan : Spatial
{
    private IList<PhysicalCard> _cards;
    private PackedScene _cardScene;
    private const float Radius = 3f;

    public override void _Ready()
    {
        _cardScene = ResourceLoader.Load<PackedScene>("res://Prefabs/Cards/PhysicalCard.tscn");
        _cards = new List<PhysicalCard>();

        for (int i = 0; i < 10; i++)
            DevCreateCard();

        Place();
    }

    private void Place()
    {
        var i = 0;
        var baseVector = new Vector3(0, Radius, 0);
        var angleBetweenCardsRad = Mathf.Tau / _cards.Count;

        foreach (var card in _cards)
        {
            var rotated = baseVector.Rotated(Vector3.Forward, i * angleBetweenCardsRad);
            card.Transform = new Transform(Transform.basis, rotated);

            card.RotateZ(-i * angleBetweenCardsRad);
            // var x = card.Rotation.Rotated(Vector3.Forward, i * angleBetweenCardsRad);
            // card.Rotation = x;

            // GD.Print(x);
            i++;
        }
    }

    private void DevCreateCard()
    {
        var card = _cardScene.Instance<PhysicalCard>();
        card.SetAsStatic();
        AddChild(card);
        _cards.Add(card);
    }
}
