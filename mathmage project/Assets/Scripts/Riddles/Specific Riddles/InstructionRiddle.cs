
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstructionRiddle : Riddle
{

    [SerializeField] DropSpot[] dropSpots;
    [SerializeField] TextMeshProUGUI mainText;

    Transform parent;

    Dictionary<string, DropSpot> dsDict = new Dictionary<string, DropSpot>();

    bool CreativeCorrect
    {
        get
        {
            if (dsDict["sin_pow"].actualValue == "1" && dsDict["sin_mult"].actualValue == "sin" && Correct) return true;
            return false;
        }
    }

    bool CreativeIncorrect
    {
        get
        {
            if (dsDict["sin_mult"].actualValue == "0" && dsDict["cos_cosmult"].actualValue == "1" && dsDict["cosmult_cos"].actualValue == "1") return true;
            return false;
        }
    }

    public override bool Correct
    {
        get
        {
            foreach (DropSpot dropSpot in dropSpots)
            {
                if (!dropSpot.ValueIsCorrect) return false;
            }
            if (((dsDict["sin_pow"].actualValue == "2" && dsDict["sin_mult"].actualValue == "1") || (dsDict["sin_pow"].actualValue == "1" && dsDict["sin_mult"].actualValue == "sin")) &&
                ((dsDict["cosmult_cos"].actualValue == "1" && dsDict["cos_cosmult"].actualValue == "cos") || (dsDict["cosmult_cos"].actualValue == "cos" && dsDict["cos_cosmult"].actualValue == "1")))
            {
                return true;
            }
            return false;
        }
    }

    public override void ChangeDesign()
    {
        if (CreativeCorrect)
        {
            mainText.text = "Ez egy nem-alap megold�s, ami m�g elfogadhat�.";
        }
        else if (Correct)
        {
            mainText.text = "Ez az alap helyes megold�s.";
        }
        else if (CreativeIncorrect)
        {
            mainText.text = "Ez egy kreat�v megold�s, �s matematikailag helyes, de nem haszn�lja fel az 'elv�rt' tud�st, nem sz�m�t helyesnek. (majd az igazi �tmutat�sz�vegben m�sk�pp lesz ez le�rva)";
        }
    }

    protected override void Start()
    {
        if (mainText == null || dropSpots == null) throw new System.Exception();
        foreach (DropSpot ds in dropSpots) dsDict.Add(ds.transform.name, ds);
        parent = transform.parent;
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            parent.gameObject.SetActive(false);
        }
        if (ValuesChanged)
        {
            ChangeDesign();
        }
    }

    public override List<object> SaveData()
    {
        return null;
    }

    public override void LoadData(List<object> data)
    {
        return;
    }
}
