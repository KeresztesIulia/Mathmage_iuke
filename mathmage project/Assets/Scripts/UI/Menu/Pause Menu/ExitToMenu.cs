using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMenu : ButtonScript
{
    [SerializeField] string sceneName = "MainMenu";
    public override void OnClick()
    {
        if (GameFunctions.SaveGame == null) return;

        GameFunctions.SaveGame();
        GameFunctions.PlayerActive = true;
        SceneManager.LoadScene(sceneName);
    }
}
