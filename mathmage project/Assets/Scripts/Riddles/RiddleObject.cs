using UnityEngine;

/// <summary>
/// The object that, if interacted with, opens the Riddle
///     - reacting to it gives the next RiddleHint
/// </summary>
public class RiddleObject : MonoBehaviour, IReactable, IActivatable
{
    [SerializeField] protected Riddle riddle;
    [SerializeField] new protected string name;
    [SerializeField] protected string activationText;
    protected SubtitleHandler subtitleHandler;

    public string Name { get { return name; } }
    public string ActivationText { get { return activationText; } }

    protected virtual void Start()
    {
        subtitleHandler = FindAnyObjectByType<SubtitleHandler>();
    }

    public virtual void Activate()
    {
        GameFunctions.PlayerActive = false;
        riddle.gameObject.SetActive(true);
    }
    
    public virtual void React()
    {
        if (subtitleHandler != null)
        {
            subtitleHandler.AddSubtitle(riddle.nextHint);
        }
    }
}
