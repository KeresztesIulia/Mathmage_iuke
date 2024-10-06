using System;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : SceneLoaderButton
{
    void Start()
    {
        if (PlayerPrefs.GetString(GameFunctions.usernameString) == "") PlayerPrefs.SetString(GameFunctions.usernameString, "guest");
        CheckUsername();
    }

    private void OnDisable()
    {
        LoginScript.UsernameChanged -= CheckUsername;
    }

    private void OnEnable()
    {
        LoginScript.UsernameChanged += CheckUsername;
    }

    public override void OnClick()
    {
        PlayerPrefs.SetInt(GameFunctions.NewGameString, 0);
        GameFunctions.saveDate = DateTime.Now;
        GameFunctions.LoadScene(sceneName);
    }

    void CheckUsername()
    {
        Button thisButton = GetComponent<Button>();
        if(thisButton != null)
        {
            if (GameFunctions.UserSaveExists())
            {
                thisButton.interactable = true;
            }
            else
            {
                thisButton.interactable = false;
            }
        }
    }

}
