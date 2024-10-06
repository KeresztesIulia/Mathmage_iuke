using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class F1LightFoodRiddle : Riddle
{
    [SerializeField] Image circleBorder;
    [SerializeField] Color correctColor;
    [SerializeField] Color wrongColor;
    [SerializeField] GameObject foodOnCanvas;
    [SerializeField] GameObject foodObject;

    
    Dictionary<string, DropSpot> dsDict;

    protected bool GotFood
    {
        set
        {
            if (value) GameFunctions.F1HasFood = true;
            foodOnCanvas.SetActive(false);
            foodObject.SetActive(false);
            solved = value;
        }
    }

    public override bool Correct
    {
        get
        {
            foreach (DropSpot dropSpot in allDropSpots)
            {
                if (!dropSpot.ValueIsCorrect) return false;
            }

            // x(t), y(t) check
            if (!((dsDict["xb_cos"].actualValue == "cos" || dsDict["xcos_b"].actualValue == "cos") && (dsDict["yb_sin"].actualValue == "sin" || dsDict["ysin_b"].actualValue == "sin")))
            {
                return false;
            }

            // integral check
            if ((dsDict["int_1"].actualValue == "y(t)" && dsDict["int_2"].actualValue == "x'(t)") || (dsDict["int_1"].actualValue == "x'(t)" && dsDict["int_2"].actualValue == "y(t)"))
            {
                if (dsDict["int_mult"].actualValue == "2" && (isPI("int_A") && dsDict["int_B"].actualValue == "0"))
                {

                    return true;
                }
                else if (dsDict["int_mult"].actualValue == "1" && (isPI("int_A", true) && dsDict["int_B"].actualValue == "0"))
                {
                    return true;
                }
                else if (dsDict["int_mult"].actualValue == "-1" && (dsDict["int_A"].actualValue == "0" && isPI("int_B", true)))
                {
                    return true;
                }
                else if (dsDict["int_mult"].actualValue == "-2" && (dsDict["int_A"].actualValue == "0" && isPI("int_B")))
                {
                    return true;
                }
                return false;
            }
            else if ((dsDict["int_1"].actualValue == "y'(t)" && dsDict["int_2"].actualValue == "x(t)") || (dsDict["int_1"].actualValue == "x(t)" && dsDict["int_2"].actualValue == "y'(t)"))
            {
                if (dsDict["int_mult"].actualValue == "-2" && (isPI("int_A") && dsDict["int_B"].actualValue == "0"))
                {
                    return true;
                }
                else if (dsDict["int_mult"].actualValue == "-1" && (isPI("int_A", true) && dsDict["int_B"].actualValue == "0"))
                {
                    return true;
                }
                else if (dsDict["int_mult"].actualValue == "1" && (dsDict["int_A"].actualValue == "0" && isPI("int_B", true)))
                {
                    return true;
                }
                else if (dsDict["int_mult"].actualValue == "2" && (dsDict["int_A"].actualValue == "0" && isPI("int_B")))
                {
                    return true;
                }
                return false;
            }

            return false;
        }
    }

    bool isPI(string dictKey, bool twopi = false)
    {
        if (!dsDict.ContainsKey(dictKey)) return false;
        if (twopi)
        {
            if (dsDict[dictKey].actualValue == "2pi" || dsDict[dictKey].actualValue == "6.28") return true;
        }
        else
        {
            if (dsDict[dictKey].actualValue == "pi" || dsDict[dictKey].actualValue == "3.14") return true;
        }
        return false;

    }

    public override void ChangeDesign()
    {
        base.ChangeDesign();
        if (Correct)
        {
            circleBorder.color = correctColor;
            GotFood = true;
        }
        else circleBorder.color = wrongColor;
    }

    protected override void Start()
    {
        base.Start();
        dsDict = new Dictionary<string, DropSpot>();
        foreach (DropSpot dropSpot in allDropSpots)
        {
            dsDict.Add(dropSpot.transform.name, dropSpot);
        }
    }

}
