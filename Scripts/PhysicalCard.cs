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
        _label = GetNode<Label>("ColorRect/Label");
    }
}
