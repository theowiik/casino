using Godot;

public sealed class CardSpriteContainer : Spatial
{
    public override void _Ready()
    {
        var frontSprite = GetNode<Sprite3D>("FrontSprite3D");
        var cardFront = GetNode<Viewport>("FrontSprite3D/FrontViewport");
        frontSprite.Texture = cardFront.GetTexture();

        var backSprite = GetNode<Sprite3D>("BackSprite3D");
        var cardBack = GetNode<Viewport>("BackSprite3D/BackViewport");
        backSprite.Texture = cardBack.GetTexture();
    }
}
