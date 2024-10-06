using System;
using UnityEngine;
using UnityEngine.UI;

public class NewGameButton : SceneLoaderButton
{
    [SerializeField] GameObject existingUserPopup;
    [SerializeField] GameObject guestUserPopup;


    //!!! only for testing
    private void Update()
    {
        if (PlayerPrefs.GetString(GameFunctions.usernameString) == "guest") GetComponent<Button>().interactable = false;
        else GetComponent<Button>().interactable = true;
    }

    public override void OnClick()
    {
        if (PlayerPrefs.GetString(GameFunctions.usernameString) != "guest")
        {
            if (GameFunctions.UserSaveExists())
            {
                existingUserPopup.SetActive(true);
            }
            else
            {
                PlayerPrefs.SetInt(GameFunctions.NewGameString, 1);
                GameFunctions.saveDate = DateTime.Now;
                GameFunctions.LoadScene(sceneName);
            }
        }
        //else guestUserPopup.SetActive(true); //!!! disabled for testing!
        
    }
}
