using UnityEngine;

public abstract class PhasedObjectDelegate : MonoBehaviour, IActivatable
{
    protected delegate void Method();
    protected delegate bool BoolExpression();
    
    [SerializeField] new protected string name;
    [SerializeField] string activationText;
    protected static int noOfPhases;
    protected int currentPhase = 0;

    protected BoolExpression[] phaseTriggers;
    protected Method[] phaseActions;

    public string Name { get { return name; } }
    public string ActivationText { get { return activationText; } }

    protected virtual void Start()
    {
        phaseTriggers = new BoolExpression[noOfPhases - 1];
        InitPhaseTriggers();
        phaseActions = new Method[noOfPhases];
        InitPhaseActions();
    }

    protected abstract void InitPhaseTriggers();
    protected abstract void InitPhaseActions();

    protected virtual void Update()
    {
        if (currentPhase < phaseTriggers.Length && phaseTriggers[currentPhase]()) currentPhase++;
    }

    // will repeat last phaseAction for all out-of-index phase numbers
    // shouldn't really be any, right?
    public void Activate()
    {
        if (phaseActions == null) throw new System.Exception("No phase actions provided!"); 
        int i = currentPhase > phaseActions.Length ? phaseActions.Length - 1 : currentPhase;
        phaseActions[i]();
    }
}
