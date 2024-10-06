using UnityEngine;
using TMPro;

public class F1TeleportRiddle : Riddle
{
    [SerializeField] DropSpot r;
    [SerializeField] string correctText;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] F1TeleportVisualizer visualizer;

    public override bool Correct
    {
        get
        {
            if (r.actualValue == "lemniscate")
            {
                foreach (DropSpot ds in r.ActualDraggable.GetComponentsInChildren<DropSpot>()) if (!ds.ValueIsCorrect) return false;
                return true;
            }
            return false;
        }
    }

    protected override void Start()
    {
        base.Start();
        checkCorrectnessAtUpdate = false;
    }

    protected override void Update()
    {
        base.Update();
        if (visualizer.TooBig)
        {
            buttonText.text = "A forma nem fér fel a teleportra";
            return;
        }
        if (visualizer.ActualCurve == GameFunctions.PolarCurve.None)
        {
            buttonText.text = "Nem számít a forma";
        }
        else
        {
            buttonText.text = "Ezt a formát szeretném rászövetni a teleportra";
        }
    }

    public void OnClick()
    {
        if (!visualizer.TooBig)
        {
            solved = true;
            if (Correct)
            {
                subtitleHandler.AddSubtitle(correctText);
            }
            GameFunctions.F1TeleportCurve = visualizer.ActualCurve;
            GameFunctions.PlayerActive = true;
            gameObject.SetActive(false);
        }
        
    }

}
