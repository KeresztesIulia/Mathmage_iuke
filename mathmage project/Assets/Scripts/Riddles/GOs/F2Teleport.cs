using Unity.VisualScripting;
using UnityEngine;

public class F2Teleport : PhasedObject, IReactable
{
    [SerializeField] string doneText;
    [SerializeField] Vector3 teleportTo;
    protected override void Start()
    {
        try
        {
            phaseObjects[1].GetComponent<F1TeleportChangeRiddle>();
            subtitleHandler = FindObjectOfType<SubtitleHandler>();
        }
        catch
        {
            throw new System.Exception("The riddle is not the one expected!");
        }
    }

    protected override void Update()
    {
        if (currentPhase == 0 && ((F1TeleportRiddle)phaseTriggers[0]).Correct) currentPhase = 2;
        base.Update();
    }

    public override void Activate()
    {
        switch (currentPhase)
        {
            case 0:
                // maybe change it up for a glitchmessage
                throw new System.Exception("The player shouldn't be able to reach this point without solving the teleport riddle once!");
            case 1:
                phaseObjects[1].SetActive(true);
                GameFunctions.PlayerActive = false;
                break;
            case 2:
                subtitleHandler.AddSubtitle(doneText);
                break;
        }
    }

    public void React()
    {
        if (currentPhase == 0) throw new System.Exception("The player shouldn't be able to reach this point without solving the teleport riddle once!");
        if (currentPhase == 1) subtitleHandler.AddSubtitle(phaseTriggers[1].nextHint);
        if (currentPhase == 2) subtitleHandler.AddSubtitle(doneText);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerControl player = other.GetComponent<PlayerControl>();
        if (player == null) player = other.GetComponentInParent<PlayerControl>();
        if (currentPhase == 2 && player != null)
        {
            player.transform.position = teleportTo;
        }
    }
}
