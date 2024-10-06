using UnityEngine;

public class PauseSaveGame : ButtonScript
{
    [SerializeField] GameObject confirmPopup;
    public override void OnClick()
    {
        if (GameFunctions.SaveGame == null) return;

        GameFunctions.SaveGame();
        confirmPopup.SetActive(true);
    }
}
