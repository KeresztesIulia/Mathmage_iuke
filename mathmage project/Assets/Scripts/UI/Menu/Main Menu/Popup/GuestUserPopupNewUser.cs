using TMPro;
using UnityEngine;

public class GuestUserPopupNewUser : ButtonScript
{
    [SerializeField] GameObject thisPopup;
    [SerializeField] GameObject existingUserPopup;
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] string sceneName = "IntroCutscene";
    public override void OnClick()
    {
        if (usernameInput.text != "" && usernameInput.text != "guest")
        {
            PlayerPrefs.SetString(GameFunctions.usernameString, usernameInput.text);
            if (GameFunctions.UserSaveExists())
            {
                existingUserPopup.SetActive(true);
                thisPopup.SetActive(false);
            }
            else
            {
                // NEWGAME STUFF
                PlayerPrefs.SetInt(GameFunctions.NewGameString, 1);
                GameFunctions.LoadScene(sceneName);
            }
        }
    }
}
