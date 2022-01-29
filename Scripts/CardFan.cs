using Godot;
using System.Collections.Generic;

public sealed class CardFan : Spatial
{
    private IList<PhysicalCard> _cards;
    private PackedScene _cardScene;

    public override void _Ready()
    {
        _cardScene = ResourceLoader.Load<PackedScene>("res://Prefabs/Cards/PhysicalCard.tscn");

        _cards = new List<PhysicalCard>();
        DevCreateCard();
    }

    private void DevCreateCard()
    {
        var card = _cardScene.Instance<PhysicalCard>();
        AddChild(card);
        _cards.Add(card);
    }
}
