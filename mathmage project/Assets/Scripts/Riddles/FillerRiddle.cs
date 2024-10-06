using UnityEngine;

public class FillerRiddle : Riddle
{
    public override bool Correct
    {
        get
        {
            return true;
        }
    }

    protected override void Start()
    {
        subtitleHandler = FindAnyObjectByType<SubtitleHandler>();
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            solved = true;
            Close();
        }        
    }
}
