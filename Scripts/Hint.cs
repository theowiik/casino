using Godot;

public sealed class Hint : Object
{
    public string Key { get; }
    public string Text { get; }
    public bool Show { get; }

    public Hint(string text, bool show = true)
    {
        Key = null;
        Text = text;
        Show = show;
    }

    public Hint(string key, string text, bool show = true)
    {
        Key = key;
        Text = text;
        Show = show;
    }

    public static Hint Hidden => new Hint("", "", false);
}
