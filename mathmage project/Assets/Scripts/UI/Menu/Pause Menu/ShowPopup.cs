using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopup : ButtonScript
{
    [SerializeField] GameObject popup;

    public override void OnClick()
    {
        popup.SetActive(true);
    }
}
