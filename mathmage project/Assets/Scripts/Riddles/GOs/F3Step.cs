using System.Collections.Generic;
using UnityEngine;

public abstract class F3Step : MonoBehaviour
{
    [SerializeField] protected F3StairRiddles riddle;

    private void Update()
    {
        ChangeStep(riddle.stairData);
    }

    protected abstract void ChangeStep(List<object> stepData);
}
