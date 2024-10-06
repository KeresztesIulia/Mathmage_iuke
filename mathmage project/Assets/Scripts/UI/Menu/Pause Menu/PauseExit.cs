using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseExit : ButtonScript
{
    
    public override void OnClick()
    {
        if (GameFunctions.SaveGame == null) return;

        GameFunctions.SaveGame();
        Application.Quit();
    }
}
