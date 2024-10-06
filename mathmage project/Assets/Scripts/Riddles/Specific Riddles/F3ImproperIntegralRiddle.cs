using System.Collections.Generic;
using UnityEngine;

public class F3ImproperIntegralRiddle : F3StairRiddles
{
    [SerializeField] float correctResult;
    enum DropSpots { A = 0, B = 1, RESULT_US = 2, RESULT_UB = 3, RESULT_L = 4, ROOT_RADICAND = 6, ADD1 = 7, ADD2 = 8, SUBTRACT1 = 9, SUBTRACT2 = 10 }
    
    public override bool Correct
    {
        get
        {
            foreach (DropSpot ds in allDropSpots) if (!ds.ValueIsCorrect) return false;

            try
            {
                float result_uppersmall = float.Parse(allDropSpots[(int)DropSpots.RESULT_US].actualValue);
                DropSpot result_upperbig = allDropSpots[(int)DropSpots.RESULT_UB];
                float result_lower = float.Parse(allDropSpots[(int)DropSpots.RESULT_L].actualValue);
                float radicand = float.Parse(allDropSpots[(int)DropSpots.ROOT_RADICAND].actualValue);
                DropSpot add1 = allDropSpots[(int)DropSpots.ADD1];
                DropSpot add2 = allDropSpots[(int)DropSpots.ADD2];
                DropSpot subtract1 = allDropSpots[(int)DropSpots.SUBTRACT1];
                DropSpot subtract2 = allDropSpots[(int)DropSpots.SUBTRACT2];

                if (!result_upperbig.ContainsDraggable) return false;
                
                if (result_uppersmall * result_lower > 0)
                {
                    if (result_upperbig.ActualDraggable.name == "subtractDraggable")
                    {
                        if (!(subtract1.ContainsDraggable ^ subtract2.ContainsDraggable)) return false;
                        float s1Value = float.Parse(subtract1.actualValue);
                        float s2Value = float.Parse(subtract2.actualValue);

                        if (subtract1.ContainsDraggable && subtract1.ActualDraggable.name == "rootDraggable")
                        {
                            if (radicand > 0 || s2Value != -1) return false;
                        }
                        else if (subtract2.ContainsDraggable && subtract2.ActualDraggable.name == "rootDraggable")
                        {
                            if (radicand < 0 || s1Value != 1) return false;
                        }
                        else return false;
                    }
                    else if (result_upperbig.ActualDraggable.name == "addDraggable")
                    {
                        if (!add1.ContainsDraggable & !add2.ContainsDraggable) return false;
                        float a1Value = float.Parse(add1.actualValue);
                        float a2Value = float.Parse(add2.actualValue);

                        if ((add1.ContainsDraggable && add1.ActualDraggable.name == "rootDraggable") || (add2.ContainsDraggable && add2.ActualDraggable.name == "rootDraggable"))
                        {
                            if (radicand > 0 || !(a1Value == 1 || a2Value == 1)) return false;
                        }
                        else return false;
                    }
                    else return false;
                }
                else
                {
                    if (result_upperbig.ActualDraggable.name == "subtractDraggable")
                    {
                        if (!(subtract1.ContainsDraggable ^ subtract2.ContainsDraggable)) return false;
                        float s1Value = float.Parse(subtract1.actualValue);
                        float s2Value = float.Parse(subtract2.actualValue);

                        if (subtract1.ContainsDraggable && subtract1.ActualDraggable.name == "rootDraggable")
                        {
                            if (radicand < 0 || s2Value != 1) return false;
                        }
                        else if (subtract2.ContainsDraggable && subtract2.ActualDraggable.name == "rootDraggable")
                        {
                            if (radicand > 0 || s1Value != -1) return false;
                        }
                        else return false;
                    }
                    else if (result_upperbig.ActualDraggable.name == "addDraggable")
                    {
                        if (!add1.ContainsDraggable & !add2.ContainsDraggable) return false;
                        float a1Value = float.Parse(add1.actualValue);
                        float a2Value = float.Parse(add2.actualValue);

                        if ((add1.ContainsDraggable && add1.ActualDraggable.name == "rootDraggable") || (add2.ContainsDraggable && add2.ActualDraggable.name == "rootDraggable"))
                        {
                            if (radicand < 0 || !(a1Value == -1 || a2Value == -1)) return false;
                        }
                        else return false;
                    }
                    else return false;
                }
            }
            catch {  return false; }
            solved = true;
            return true;
        }
    }

    public override List<object> stairData
    {
        get
        {
            float maxA = 17, minB = 10;
            bool successful = true;
            successful &= float.TryParse(allDropSpots[(int)DropSpots.A].actualValue, out float a);
            successful &= float.TryParse(allDropSpots[(int)DropSpots.B].actualValue, out float b);
            if (successful)
                return new List<object>
                {
                    maxA,
                    a,
                    minB,
                    b
                };
            else
                return new List<object>
                {
                    maxA,
                    0,
                    minB,
                    0
                };
        }
    }

}
