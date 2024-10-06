
using System.Collections.Generic;

public class F3OpenWindowRiddle : F3StairRiddles
{

    float prevA = 0;
    float prevB = 0;

    public override bool Correct
    {
        get
        {
            foreach (DropSpot ds in allDropSpots) if (!ds.ValueIsCorrect) return false;
            return true;
        }
    }

    public float A
    {
        get
        {
            try
            {
                return float.Parse(allDropSpots[0].actualValue);
            }
            catch
            {
                return 0;
            }
            
     } }
    public float B
    {
        get
        {
            try
            {
                return float.Parse(allDropSpots[1].actualValue);
            }
            catch
            {
                return 0;
            }

        }
    }

    public bool ValueChanged
    {
        get
        {
            return prevA != A && prevB != B;
        }
    }

    public override List<object> stairData
    {
        get
        {
            return new List<object>{
                A,
                B
            };
        }
    }

    protected override void Update()
    {
        base.Update();
        if (ValueChanged)
        {
            prevA = A;
            prevB = B;
            //ChangeDesign();
        }
    }
}
