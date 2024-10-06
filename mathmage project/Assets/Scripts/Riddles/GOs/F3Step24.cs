using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F3Step24 : F3Step
{
    [SerializeField] GameObject step;
    protected override void ChangeStep(List<object> stepData)
    {
        step.SetActive((bool)stepData[0]);
    }
}
