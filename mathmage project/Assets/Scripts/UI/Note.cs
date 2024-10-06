using UnityEngine;

public class Note : Closeable, IOneTimeReact
{
    [SerializeField] protected string reaction;
    protected SubtitleHandler subtitleHandler;
    protected bool hasReacted = false;

    public string Reaction { get { return reaction; } }
    public bool HasReacted { get {  return hasReacted; } }

    private void Start()
    {
        subtitleHandler = FindAnyObjectByType<SubtitleHandler>();
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OneTimeReact();
            Close();
        }
    }

    public void OneTimeReact()
    {
        if (!hasReacted)
        {
            hasReacted = true;
            if (subtitleHandler != null) subtitleHandler.AddSubtitle(reaction);
        }
    }
}
