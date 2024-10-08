using UnityEngine;


public class F2LabyrinthDoor : RiddleObject
{
    float startTime = 0;
    static float disappearanceTime = 0.9f;
    Vector3 startScale;

    [SerializeField] Transform door;
    [SerializeField] GameObject reminderObject;
    [SerializeField] Closeable reminderUI;

    bool switchedState = false;

    float elapsedTime
    {
        get
        {
            return Time.time - startTime;
        }
    }

    protected override void Start()
    {
        base.Start();
        if (riddle.GetType() != typeof(F2LabyrinthDoorRiddle)) throw new System.Exception("Has to be a labyrinth door riddle!");
        startScale = door.localScale;
    }

    private void Update()
    {
        if (riddle.Solved)
        {
            if (startTime == 0) startTime = Time.time;
            if (elapsedTime < disappearanceTime)
            {
                float relativeScale = (disappearanceTime - elapsedTime) / disappearanceTime;
                door.localScale = startScale * relativeScale;
            }
            else
            {
                if (!switchedState)
                {
                    Destroy(door.gameObject);
                    reminderObject.SetActive(true);
                    name = "Emlékeztető";
                    switchedState = true;
                }
                
            }
        }
    }

    public override void Activate()
    {
        if (!riddle.Solved) base.Activate();
        else
        {
            GameFunctions.PlayerActive = false;
            reminderUI.gameObject.SetActive(true);
        }
    }
}
