using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F5CryptogramRiddle : Riddle
{
    [SerializeField] F5ABCRiddle[] requisites; // we'll see if we will link it to that or we let people solve it however they want
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

    //public override void LoadData(List<object> data)
    //{
    //    base.LoadData(data);
    //}
}
