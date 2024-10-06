using UnityEngine;
using TMPro;

public class F1TeleportChangeRiddle : Riddle
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
        if (visualizer.ActualCurve == GameFunctions.F1TeleportCurve && GameFunctions.F1TeleportCurve != GameFunctions.PolarCurve.None)
        {
            buttonText.text = "Ez a görbe van most a teleporton";
            return;
        }
        if (visualizer.TooBig)
        {
            buttonText.text = "A görbe nem fér fel a teleportra";
            return;
        }
        if (visualizer.ActualCurve == GameFunctions.PolarCurve.None)
        {
            buttonText.text = "Nidre hagyom, hogy szőjjön amit akar";
        }
        else
        {
            buttonText.text = "Ezt a görbét szeretném rászövetni a teleportra";
        }
    }

    public void OnClick()
    {
        if (!visualizer.TooBig && (visualizer.ActualCurve != GameFunctions.F1TeleportCurve || GameFunctions.F1TeleportCurve == GameFunctions.PolarCurve.None))
        {
            if (Correct)
            {
                solved = true;
                subtitleHandler.AddSubtitle(correctText);
            }
            GameFunctions.F1TeleportCurve = visualizer.ActualCurve;
            GameFunctions.PlayerActive = true;
            gameObject.SetActive(false);
        }
        
    }

}
