using System.Collections.Generic;
using UnityEngine;


public class F5ABCRiddle : Riddle
{
    [SerializeField] int[] neededLetters;
    [SerializeField] int[] piecesNeededPerLetter;

    public override bool Correct
    {
        get
        {
            foreach (int i in neededLetters)
            {
                if (i == -1) break;
                if (!allDropSpots[i].ValueIsCorrect) return false;
            }
            return true;
        }
    }

    protected override void Start()
    {
        foreach (DropSpot ds in allDropSpots)
        {
            ds.transform.parent.gameObject.SetActive(false);
        }
        base.Start();
    }

    public void FoundPiece(int letter)
    {
        piecesNeededPerLetter[letter]--;
        if (piecesNeededPerLetter[letter] == 0) allDropSpots[letter].transform.parent.gameObject.SetActive(true);
    }

    public override List<object> SaveData()
    {
        return new List<object>{
            base.SaveData()
            //piecesNeededPerLetter
        };
    }

    public override void LoadData(List<object> data)
    {
        base.LoadData((List<object>)data[0]);
        //piecesNeededPerLetter = (int[])data[1];
        //for (int i = 0; i < piecesNeededPerLetter.Length; i++)
        //{
        //    if (piecesNeededPerLetter[i] == 0) allDropSpots[i].gameObject.SetActive(true);
        //}
    }
}
