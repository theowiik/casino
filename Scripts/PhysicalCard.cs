using Godot;

public sealed class PhysicalCard : RigidBody, IHoverable
{
    [Signal]
    public delegate void BeingHovered(PhysicalCard card);

    public string Text
    {
        set
        {
            _label.Text = value;
        }
    }

    private Label _label;

    public override void _Ready()
    {
        var frontSprite = GetNode<Sprite3D>("FrontSprite3D");
        var cardFront = GetNode<Viewport>("FrontSprite3D/FrontViewport");
        frontSprite.Texture = cardFront.GetTexture();

        var backSprite = GetNode<Sprite3D>("BackSprite3D");
        var cardBack = GetNode<Viewport>("BackSprite3D/BackViewport");
        backSprite.Texture = cardBack.GetTexture();

        // TODO: Create a model for card front
        _label = GetNode<Label>("FrontSprite3D/FrontViewport/CardFront/ColorRect/Label");
    }

    public void HoverStarted()
    {
        EmitSignal(nameof(BeingHovered), this);
    }

    public void HoverEnded() { }

    public void SetAsStatic()
    {
        Mode = RigidBody.ModeEnum.Static;
    }

    public void SetAsRigid()
    {
        Mode = RigidBody.ModeEnum.Rigid;
    }
}
