using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGamePopupButton : SceneLoaderButton
{
    public override void OnClick() { }
    public void OnClick(bool isNewGame)
    {
        int newGameInt = isNewGame ? 1 : 0;
        PlayerPrefs.SetInt(GameFunctions.NewGameString, newGameInt);
        SceneManager.LoadScene(sceneName);
    }
}
