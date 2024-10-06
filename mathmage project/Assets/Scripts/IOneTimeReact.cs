
public interface IOneTimeReact
{
    public string Reaction { get; }
    public bool HasReacted { get; }

    void OneTimeReact();
}
