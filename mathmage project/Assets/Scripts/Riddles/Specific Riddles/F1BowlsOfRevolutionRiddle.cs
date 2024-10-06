using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class F1BowlsOfRevolutionRiddle : Riddle
{
    [SerializeField] GameObject[] inactivateAtClose;
    public int activeBOR = -1;
    public int foodBowlIndex = -1;

    [Header("Dropspots")]
    [SerializeField] DropSpot[] common;
    [SerializeField] DropSpot[] bor1ds;
    [SerializeField] DropSpot[] bor2ds;
    [SerializeField] DropSpot[] bor3ds;
    [SerializeField] DropSpot[] bor4ds;
    [SerializeField] DropSpot[] bor5ds;

    [Header("Stuff that shows up")]
    [SerializeField] GameObject addFoodText;
    [SerializeField] GameObject removeFoodText;

    string[] previousValues;
    int borCount = 5;

    DropSpot[][] borDropspots;

    bool[] solvedBOR;

    public override bool Correct
    {
        get
        {
            if (activeBOR == -1) return false;
            return CorrectBOR(activeBOR);
        }
    }

    protected override void Start()
    {
        base.Start();
        solvedBOR = new bool[borCount];
        for (int i = 0; i < borCount; i++) solvedBOR[i] = false;
        borDropspots = new DropSpot[borCount][];
        borDropspots[0] = bor1ds;
        borDropspots[1] = bor2ds;
        borDropspots[2] = bor3ds;
        borDropspots[3] = bor4ds;
        borDropspots[4] = bor5ds;

        previousValues = new string[common.Length + bor1ds.Length + 1];
    }
    protected bool CorrectBOR(int index)
    {
        if (index >= borCount) throw new System.Exception("Not a valid index!");
        foreach (DropSpot ds in common) if (!ds.ValueIsCorrect) return false;
        foreach (DropSpot ds in borDropspots[index]) if (!ds.ValueIsCorrect) return false;
        string upper = borDropspots[index][0].actualValue;
        string lower = borDropspots[index][1].actualValue;
        if (upper.Equals(lower)) return false;
        bool piPositive = isPIofType(common[0].actualValue);
        bool upperSmallerThanLower;

        

        switch (index)
        {
            case 0:
                if (lower.Equals("0") && isPIofType(upper, "pi/2")) upperSmallerThanLower = false;
                else if (isPIofType(lower, "pi/2") && upper.Equals("0")) upperSmallerThanLower = true;
                else return false;
                break;
            case 1:
                if (lower.Equals("0") && isPIofType(upper, "pi/4")) upperSmallerThanLower = false;
                else if (isPIofType(lower, "pi/4") && upper.Equals("0")) upperSmallerThanLower = true;
                else return false;
                break;
            case 2:
            case 3:
            case 4:
                float upperNum = float.Parse(upper);
                float lowerNum = float.Parse(lower);
                upperSmallerThanLower = upperNum < lowerNum;
                break;
            default:
                throw new System.Exception("Not a valid index!");
        }
        if (!upperSmallerThanLower ^ piPositive) return false;
        solvedBOR[index] = true;
        return true;
    }

    bool isPIofType(string value, string piType = "pi")
    {
        if (piType.Equals(value)) return true;
        if (piType.Contains('-') ^ value.Contains('-')) return false;

        if (piType.Contains('-'))
        {
            value = value.Substring(1);
            piType = piType.Substring(1);
        }
        float number = float.Parse(value);
        float piValue = 3.14f;
        switch (piType)
        {
            case "pi":
                return number == piValue;
            case "pi/2":
                return number == piValue/2;
            case "pi/4":
                return number == piValue/4;
            default:
                throw new System.Exception("Not a valid pi type for this riddle!");
        }
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) foreach (GameObject inactivate in inactivateAtClose) inactivate.SetActive(false);
        base.Update();
        if (activeBOR == -1) return;
      
        if (solvedBOR[activeBOR] && GameFunctions.F1HasFood)
            if (foodBowlIndex == activeBOR)
            {
                addFoodText.SetActive(false);
                removeFoodText.SetActive(true);
            }
            else
            {
                addFoodText.SetActive(true);
                removeFoodText.SetActive(false);
            }
        else
        {
            addFoodText.SetActive(false);
            removeFoodText.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E) && solvedBOR[activeBOR] && GameFunctions.F1HasFood)
        {
            if (foodBowlIndex == activeBOR)
            {
                foodBowlIndex = -1;

                addFoodText.SetActive(true);
                removeFoodText.SetActive(false);
            }
            else
            {
                foodBowlIndex = activeBOR;

                addFoodText.SetActive(false);
                removeFoodText.SetActive(true);
            }
        }
        solved = foodBowlIndex == 0;
    }

    protected override bool ValuesChanged
    {
        get
        {
            if (previousValues[0] != activeBOR.ToString())
            {
                previousValues[0] = activeBOR.ToString();
                int i = 1;
                foreach (DropSpot ds in common)
                {
                    previousValues[i] = ds.actualValue; i++;
                }
                foreach (DropSpot ds in borDropspots[activeBOR])
                {
                    previousValues[i] = ds.actualValue;
                    i++;
                }
                return true;
            }
            else
            {
                bool changed = false;
                int i = 1;
                foreach (DropSpot ds in common)
                {
                    if (ds.actualValue != previousValues[i])
                    {
                        previousValues[i] = ds.actualValue;
                        changed = true;
                    }
                    i++;
                }
                foreach (DropSpot ds in borDropspots[activeBOR])
                {
                    if (ds.actualValue != previousValues[i])
                    {
                        previousValues[i] = ds.actualValue;
                        changed = true;
                    }
                    i++;
                }
                return changed;
            }
        }
    }
    

    public override List<object> SaveData()
    {
        return new List<object>
        {
            base.SaveData(),
            GameFunctions.F1HasFood
        };
        
    }

    public override void LoadData(List<object> data)
    {
        base.LoadData((List<object>)data[0]);
        GameFunctions.F1HasFood = (bool)data[1];

    }
}
