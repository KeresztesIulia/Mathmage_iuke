using System.Collections.Generic;
using UnityEngine;

public class Spider : PhasedObject, ISaveable
{
    [SerializeField] Riddle fillerReplacer;
    bool savedMageKnowledge = false;

    protected override void Update()
    {
        if (phaseObjects[4].GetComponent<ReactOnFirstEncounter>().Reacted && !savedMageKnowledge) GameFunctions.MageIsNotHereKnown = true;
        if (currentPhase == 2 && fillerReplacer.Solved) currentPhase++;
        base.Update();
    }

    public List<object> SaveData()
    {
        return new List<object>
        {
            GameFunctions.MageIsNotHereKnown
        };
    }

    public void LoadData(List<object> data)
    {
        GameFunctions.MageIsNotHereKnown = (bool)data[0];
    }
}
