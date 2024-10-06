using UnityEngine;

public class specialValueDraggable : Draggable
{
    [SerializeField] float multiplier = 1;
    [SerializeField] float specialValue;

    public override string Value
    {
        get
        {
            return "" + (multiplier * specialValue);
        }
    }
}
