using System.Collections.Generic;
using UnityEngine;

public class F3PutBackBookRiddle : F3StairRiddles
{
    [SerializeField] F3BookSpot[] correctSpots;
    bool firstEnable = true;

    public override bool Correct
    {
        get
        {
            foreach (F3BookSpot spot in correctSpots)
            {
                if (spot.ContainsBook)
                {
                    if (solved) return true;
                    else
                    {
                        foreach (DropSpot ds in allDropSpots) if (!ds.ValueIsCorrect) return false;
                        solved = true;
                        return true;
                    }
                }
            }
            solved = false;
            return false;
        }
    }

    public override List<object> stairData
    {
        get
        {
            return new List<object> { Correct };
        }
    }

    private void OnEnable()
    {
        if (!firstEnable) GameFunctions.F3HasBook = true;
        firstEnable = false;
    }
}
