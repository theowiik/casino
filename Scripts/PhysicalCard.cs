using Godot;

public sealed class PhysicalCard : Node
{
    public string Text
    {
        get
        {
            return _text;
        }
        set
        {
            _text = value;
            _label.Text = _text;
        }
    }

    private string _text;
    private Label _label;

    public override void _Ready()
    {
        var frontSprite = GetNode<Sprite3D>("FrontSprite3D");
        var cardFront = GetNode<Viewport>("FrontSprite3D/FrontViewport");
        frontSprite.Texture = cardFront.GetTexture();

        var backSprite = GetNode<Sprite3D>("BackSprite3D");
        var cardBack = GetNode<Viewport>("BackSprite3D/BackViewport");
        backSprite.Texture = cardBack.GetTexture();

        _label = GetNode<Label>("ColorRect/Label");
    }
}
