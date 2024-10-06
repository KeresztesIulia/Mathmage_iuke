using UnityEngine;

public class DELETEPLAYERPREFS : ButtonScript
{
    public override void OnClick()
    {
        PlayerPrefs.DeleteAll();
    }
}
