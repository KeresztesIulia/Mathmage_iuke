using System.Collections.Generic;
using UnityEngine;

public class Spider : PhasedObject, ISaveable
{
    // DEBUG!!!! kinda... anyway, should be deleted
    // 1 - I'm hungry
    // 2 - tenkjú, I'm going to eat the food now
    // 3 - what sign should I make on the teleport - mark it on the teleport and I will make it
    // 4 - done, you can now use the teleport - (maybe something about the mage not liking visitors, but it's weird that he hasn't come down for a while now)
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
