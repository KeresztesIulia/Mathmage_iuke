using UnityEngine;

public class BowlOfRevolution : RiddleObject
{
    [SerializeField] GameObject borSpecificPart;
    [SerializeField] int index;

    protected override void Start()
    {
        base.Start();
        if (riddle is not F1BowlsOfRevolutionRiddle) throw new System.Exception("Not a Bowl of Revolution Riddle!");
    }

    public override void Activate()
    {
        base.Activate();
        borSpecificPart.SetActive(true);
        ((F1BowlsOfRevolutionRiddle)riddle).activeBOR = index;
    }

    private void Update()
    {
        if (((F1BowlsOfRevolutionRiddle)riddle).foodBowlIndex == index)
        {
            // show food graphic
        }
        else
        {
            // hide food graphic
        }
    }
}
