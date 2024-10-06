using System.Collections.Generic;
using UnityEngine;

public class F3GravityDevice : F3StairRiddles
{
    [SerializeField] GameObject limit;
    [SerializeField] Draggable e;

    [SerializeField] GameObject device;

    [SerializeField] int eSpotIndex;

    [SerializeField] string doneText;


    public override bool Correct
    {
        get
        {
            for (int i = 0; i < allDropSpots.Length; i++)
            {
                if (i == eSpotIndex) continue;
                if (!allDropSpots[i].ValueIsCorrect) return false;
            }
            return true;
        }
    }

    public override List<object> stairData
    {
        get
        {
            return new List<object>
            {
                GameFunctions.F3HasGravityDevice
            };
        }
    }

    public override void ChangeDesign()
    {
        base.ChangeDesign();
        solved = false;
        if (Correct)
        {
            foreach (DropSpot ds in allDropSpots)
            {
                if (ds.ContainsDraggable) ds.ActualDraggable.gameObject.SetActive(false);
            }
            limit.SetActive(false);
            e.gameObject.SetActive(true);
            e.snapPosition = e.GetComponent<RectTransform>().position;
        }
        if (allDropSpots[eSpotIndex].ValueIsCorrect)
        {
            GameFunctions.F3HasGravityDevice = true;
            device.SetActive(false);
            solved = true;
            GameFunctions.PlayerActive = true;
            Close();
            subtitleHandler.AddSubtitle(doneText);
        }
    }
}
