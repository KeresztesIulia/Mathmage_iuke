using UnityEngine;

public class GuestUserPopupNewGame : ButtonScript
{
    [SerializeField] GameObject thisPopup;
    [SerializeField] GameObject existingUserPopup;
    [SerializeField] string sceneName = "IntroCutscene";

    public override void OnClick()
    {
        if(GameFunctions.UserSaveExists())
        {
            existingUserPopup.SetActive(true);
            thisPopup.SetActive(false);
        }
        else
        {
            //NEWGAME STUFF
            PlayerPrefs.SetInt(GameFunctions.NewGameString, 1);
            GameFunctions.LoadScene(sceneName);
        }
    }
}
