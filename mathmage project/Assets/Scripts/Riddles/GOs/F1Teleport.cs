using UnityEngine;

public  class F1Teleport : PhasedObject, IReactable
{
    [SerializeField] Vector3 teleportTo;
    [SerializeField] string doneText;
    F1TeleportRiddle phase2Riddle;
    F1TeleportChangeRiddle phase3Riddle;

    protected override void Start()
    {
        try
        {
            phase2Riddle = phaseObjects[2].GetComponent<F1TeleportRiddle>();
            phase3Riddle = phaseObjects[3].GetComponent<F1TeleportChangeRiddle>();
        }
        catch
        {
            throw new System.Exception("Two phases should be riddles!");
        }
        base.Start();
    }


    public override void Activate()
    {
        if (currentPhase == 3 && (phase2Riddle.Correct || phase3Riddle.Solved)) subtitleHandler.AddSubtitle(doneText);
        else base.Activate();
    }

    public void React()
    {
        if (currentPhase == 2) subtitleHandler.AddSubtitle(phase2Riddle.nextHint);
        if (currentPhase == 3)
        {
            if (phase2Riddle.Correct || phase3Riddle.Solved)
            {
                subtitleHandler.AddSubtitle(doneText);
            }
            else
            {
                subtitleHandler.AddSubtitle(phase3Riddle.nextHint);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerControl player = other.GetComponent<PlayerControl>();
        if (player == null) player = other.GetComponentInParent<PlayerControl>();
        if (currentPhase == 3 && player != null)
        {
            player.transform.position = teleportTo;
        }
    }
}
