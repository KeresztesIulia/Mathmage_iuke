using UnityEngine;
using UnityEngine.Playables;

public sealed class ENDMagicDevice : PhasedObjectDelegate
{
    // Phases:
    //      1. "I don't know what this is, but it may not be a good idea to use it. I don't know what it does"
    //      2. Activate the device -> stop the dark magic, yay.
    //      3. Can't activate anymore. "I should go and see if everything is back to normal"
    //
    // Phase triggers:
    //      1->2. Watch video
    //      2->3. Activate the device

    [SerializeField] string videoNotWatchedText;
    [SerializeField] string endingReachedText;
    [SerializeField] PlayableDirector deviceActivationTimeline;
    SubtitleHandler subtitleHandler;

    [SerializeField] GameObject deviceBall;
    [SerializeField] GameObject DDCollection;

    protected override void Start()
    {
        noOfPhases = 3;
        base.Start();
        subtitleHandler = FindObjectOfType<SubtitleHandler>();
    }

    protected override void InitPhaseTriggers()
    {
        phaseTriggers[0] = delegate
        {
            if (GameFunctions.ENDVideoWatched)
            {
                name = "Varázslatelhárító eszköz";
                return true;
            }
            return false;
        };
        phaseTriggers[1] = delegate
        {
            if (GameFunctions.ENDDeviceActivated)
            {
                deviceBall.SetActive(false);
                DDCollection.SetActive(false);
                return true;
            }
            return false;
        };
    }

    protected override void InitPhaseActions()
    {
        phaseActions[0] = delegate
        {
            subtitleHandler.AddSubtitle(videoNotWatchedText);
        };
        phaseActions[1] = delegate
        {
            GameFunctions.PlayTimeline(deviceActivationTimeline);
        };
        phaseActions[2] = delegate
        {
            subtitleHandler.AddSubtitle(endingReachedText);
        };
    }

    public void AfterActivation()
    {
        GameFunctions.ENDDeviceActivated = true;
        GameFunctions.AfterVideo();
    }

}
