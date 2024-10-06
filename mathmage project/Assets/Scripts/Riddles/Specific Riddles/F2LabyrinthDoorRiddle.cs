using UnityEngine;


// for the UI Riddle thing
public class F2LabyrinthDoorRiddle : Riddle
{
    public override bool Correct
    {
        get
        {
            foreach (DropSpot ds in allDropSpots) if (!ds.ValueIsCorrect) return false;
            return true;
        }
    }

    public override void ChangeDesign()
    {
        if (Correct)
        {
            solved = true;
            GameFunctions.PlayerActive = true;
            Close();
        }
    }


}
