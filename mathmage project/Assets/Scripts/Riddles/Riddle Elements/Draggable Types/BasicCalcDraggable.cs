using UnityEngine;
using System;

public class BasicCalcDraggable : DraggableWithSubSpots
{
    [SerializeField] char operatorToUse = 'X';
    float[] defaultValues = { 0, 0, 1, 1, 2};
    Operators usedOperator;

    enum Operators { ADD = 0, SUBSTRACT = 1, MULTIPLY = 2, DIVIDE = 3, ROOT = 4 };

    protected override void Start()
    {
        base.Start();
        switch (operatorToUse)
        {
            case '+':
                usedOperator = Operators.ADD;
                break;
            case '-':
                usedOperator = Operators.SUBSTRACT;
                break;
            case '*':
                usedOperator = Operators.MULTIPLY;
                break;
            case '/':
                usedOperator = Operators.DIVIDE;
                break;
            case 'r':
                usedOperator = Operators.ROOT;
                break;
            default:
                throw new System.Exception("Not a valid operator for basic calculations!");

        }
    }

    public override string Value
    {
        get
        {
            float value1;
            float value2;
            try
            {
                value1 = float.Parse(subSpots[0].actualValue);

            }
            catch { value1 = defaultValues[(int)usedOperator]; }
            try
            {
                value2 = float.Parse(subSpots[1].actualValue);
            }
            catch { value2 = defaultValues[(int)usedOperator]; }
            
            switch (usedOperator)
            {
                case Operators.ADD:
                    return "" + (value1 + value2);
                case Operators.SUBSTRACT:
                    return "" + (value1 - value2);
                case Operators.MULTIPLY:
                    return "" + Math.Round(value1 * value2, 2);
                case Operators.DIVIDE:
                    if (value2 == 0) return "undefined";
                    else return "" + Math.Round(value1 / value2, 2);
                case Operators.ROOT:
                    try
                    {
                        double val = 0;
                        if (value1 % 2 != 0) val = Math.Sign(value2) * Math.Pow(Math.Abs(value2), 1 / value1);
                        else val = Mathf.Pow(value2, 1 / value1);
                        return "" + Math.Round(val,2);
                    }
                    catch {  return "undefined"; }
                    
            }
            return "undefined";
        }
    }
}
