using UnityEngine;

public class TwoSidedNote : Note
{
    [SerializeField] GameObject front;
    [SerializeField] GameObject back;

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            front.SetActive(true);
            back.SetActive(false);
        }
        base.Update();
    }
}
