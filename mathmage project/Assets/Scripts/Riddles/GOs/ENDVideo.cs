using UnityEngine;
using UnityEngine.Playables;

public class ENDVideo : PhasedObjectDelegate
{
    // Phases:
    //      1. Watch video
    //      2. "No time for this now. I can watch the rest AFTER I solve the DD problem"
    //      3. Watch the rest of the video (GameFunctions.ENDWatchedVideoTillTheEnd)
    //      4. "I think it's time I go back to see if everything is back to normal."
    //
    // Phase triggers:
    //      1->2. Watch video
    //      2->3. Activate the device
    //      3->4. Watch the rest of the video

    [SerializeField] string noTimeToWatchNowText;
    [SerializeField] string afterEndText;

    [SerializeField] PlayableDirector preActivationTimeline;
    [SerializeField] PlayableDirector postActivationTimeline;
  

    SubtitleHandler subtitleHandler;


    protected override void Start()
    {
        noOfPhases = 4;
        base.Start();
        subtitleHandler = new SubtitleHandler();
    }

    protected override void InitPhaseTriggers()
    {
        phaseTriggers[0] = delegate
        {
            if (GameFunctions.ENDVideoWatched)
            {
                name = "Ahidon utolsó üzenete";
                return true;
            }
            return false;
        };
        phaseTriggers[1] = delegate
        {
            return GameFunctions.ENDDeviceActivated;
        };
        phaseTriggers[2] = delegate
        {
            return GameFunctions.ENDVideoFinished;
        };
    }

    protected override void InitPhaseActions()
    {
        phaseActions[0] = delegate
        {
            GameFunctions.PlayTimeline(preActivationTimeline);
            
            // well never mind this idea now... forgot this was even here
            // cutscene: "Where is the Mágus? He wasn't on any of the other floors, and he's not here. Was he turned to stone too? But then... there is no way to stop this ... Huh, what is this? (takes up video thing, it starts playing)"
            // if MageIsNotHereKnown: "Where is the Mágus? Is he really not here? But then... did he turn to stone too? There is no way to stop this, then? ... Huh, what is this? (video plays)"
            
        };
        phaseActions[1] = delegate
        {
            subtitleHandler.AddSubtitle(noTimeToWatchNowText);
            // "There's no time to watch this now. Mágus said the barrier may not last long, I have to activate the device. I can watch the rest AFTER I solve the problem."
        };
        phaseActions[2] = delegate
        {
            GameFunctions.PlayTimeline(postActivationTimeline);
            
        };
        phaseActions[3] = delegate
        {
            subtitleHandler.AddSubtitle(afterEndText);
        };
    }

    public void AfterPreActivation()
    {
        GameFunctions.ENDVideoWatched = true;
        GameFunctions.AfterVideo();
    }

    public void AfterPostActivation()
    {
        GameFunctions.ENDVideoFinished = true;
        GameFunctions.AfterVideo();
    }

}
