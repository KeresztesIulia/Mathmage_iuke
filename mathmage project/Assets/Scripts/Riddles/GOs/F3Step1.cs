using System.Collections.Generic;
using UnityEngine;

public class F3Step1 : F3Step
{
    [SerializeField] Vector3 startPosition;
    [SerializeField] float scaling;
    [SerializeField] float minScaling = 0.01f;

    protected override void ChangeStep(List<object> stepData)
    {
        float a = (float)stepData[0];
        float b = (float)stepData[1];
        float interval = b - a;
        transform.localPosition = startPosition;
        transform.Translate(new Vector3(0, a * scaling, 0), Space.Self);
        
        float finalScaling = Mathf.Abs(interval*scaling) > minScaling ? interval*scaling : minScaling;
        transform.localScale = new Vector3(1, finalScaling, 1);
    }
}
