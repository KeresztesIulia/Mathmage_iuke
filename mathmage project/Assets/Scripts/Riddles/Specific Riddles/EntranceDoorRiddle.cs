using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class EntranceDoorRiddle : Riddle
{
    [SerializeField] DropSpot answer;
    [SerializeField] TMP_Text answerText;
    [SerializeField] Color correctColor;
    [SerializeField] Color baseColor;

    public override bool Correct
    {
        get
        {
            return answer.ValueIsCorrect;

        }
    }

    public override void ChangeDesign()
    {
        base.ChangeDesign();
        if (Correct)
        {
            
            answerText.color = correctColor;
        }
        else
        {
            answerText.color = baseColor;
        }
    }

}
