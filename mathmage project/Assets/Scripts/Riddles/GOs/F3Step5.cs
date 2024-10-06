
using System.Collections.Generic;
using UnityEngine;

public class F3Step5 : F3Step
{
    [SerializeField] Transform stepA;
    [SerializeField] Transform stepB;

    [SerializeField] float scaling;
    [SerializeField] float minScaling = 0.01f;

    protected override void ChangeStep(List<object> stepData)
    {

        float maxA = (float)stepData[0];
        float a = (float)stepData[1];
        float minB = (float)stepData[2];
        float b = (float)stepData[3];

        float scaleA = Mathf.Abs((a - maxA) * scaling) > minScaling ? (a - maxA) * scaling : minScaling;
        float scaleB = Mathf.Abs((minB - b) * scaling) > minScaling ? (minB - b) * scaling : minScaling;
        stepA.localScale = new Vector3(1, scaleA, 1);
        stepB.localScale = new Vector3(1, scaleB, 1);
    }
}
