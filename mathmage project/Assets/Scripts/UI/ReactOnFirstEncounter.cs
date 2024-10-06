using UnityEngine;

public class ReactOnFirstEncounter : Closeable
{

    [SerializeField] string reaction;
    SubtitleHandler subtitleHandler;
    bool reacted = false;

    public bool Reacted
    {
        get { return reacted; }
    }

    void Start()
    {
        subtitleHandler = FindObjectOfType<SubtitleHandler>();
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!reacted) subtitleHandler.AddSubtitle(reaction);
            reacted = true;
            Close();
        }
    }
}
