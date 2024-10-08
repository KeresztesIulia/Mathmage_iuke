using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F5CryptogramRiddle : Riddle
{
    [SerializeField] F5ABCRiddle[] requisites;
    [SerializeField] ButtonScript upButton;
    public override bool Correct
    {
        get
        {
            foreach (DropSpot ds in allDropSpots) if (!ds.ValueIsCorrect) return false;
            upButton.gameObject.SetActive(true);
            return true;
        }
    }

    protected override void Start()
    {
        upButton.gameObject.SetActive(false);
        base.Start();
    }

}
