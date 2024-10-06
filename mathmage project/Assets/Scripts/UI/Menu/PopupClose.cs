using UnityEngine;

public class PopupClose : ButtonScript
{
    [SerializeField] GameObject popup;
    public override void OnClick()
    {
        popup.SetActive(false);
    }
}
