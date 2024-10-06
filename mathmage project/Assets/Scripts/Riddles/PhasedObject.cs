using UnityEngine;

public class PhasedObject : MonoBehaviour, IActivatable
{
    [SerializeField] protected GameObject[] phaseObjects; // what opens at a given phase
    [SerializeField] protected Riddle[] phaseTriggers; // what makes it move from one phase to another
    [SerializeField] new string name;
    [SerializeField] string activationText;
    protected int currentPhase = 0;
    protected SubtitleHandler subtitleHandler;

    public string Name { get { return name; } }
    public string ActivationText {  get { return activationText; } }

    protected virtual void Start()
    {
        if (phaseTriggers.Length != phaseObjects.Length - 1) throw new System.Exception("Phase trigger count has to correspond to the number of phase objects!");
        subtitleHandler = FindObjectOfType<SubtitleHandler>();
    }
    protected virtual void Update()
    {
        if (currentPhase < phaseTriggers.Length && phaseTriggers[currentPhase].Solved) currentPhase++;
    }

    public virtual void Activate()
    {
        phaseObjects[currentPhase].SetActive(true);
        GameFunctions.PlayerActive = false;
    }
}
