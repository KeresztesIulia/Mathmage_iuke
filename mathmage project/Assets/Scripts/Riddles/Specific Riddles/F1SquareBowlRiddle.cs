using UnityEngine;
using UnityEngine.UI;

public class F1SquareBowlRiddle : Riddle
{
    [SerializeField] Image result;
    [SerializeField] Color rightColor;
    [SerializeField] Color wrongColor;

    public override bool Correct
    {
        get
        {
            foreach (DropSpot ds in allDropSpots)
            {
                if (!ds.ValueIsCorrect) return false;
            }
            try
            {
                float mult = 1;
                float result = 0;
                foreach (DropSpot ds in allDropSpots)
                {
                    if (ds.transform.name.Contains("num"))
                        mult *= float.Parse(ds.actualValue);
                    if (ds.transform.name.Equals("result")) result = float.Parse(ds.actualValue);
                }
                mult = Mathf.Round(mult * 100) / 100;
                if (mult != result) return false;
            }
            catch { return false; }
            solved = true;
            return true;
        }
    }

}

