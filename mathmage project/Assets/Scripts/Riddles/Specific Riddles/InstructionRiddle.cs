
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstructionRiddle : Riddle
{

    [SerializeField] DropSpot[] dropSpots;
    [SerializeField] TextMeshProUGUI mainText;

    Transform parent; // Debug! -> addig kell amíg még nincs meg a teljes instrukció-menet

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
            mainText.text = "Ez egy nem-alap megoldás, ami még elfogadható.";
        }
        else if (Correct)
        {
            mainText.text = "Ez az alap helyes megoldás.";
        }
        else if (CreativeIncorrect)
        {
            mainText.text = "Ez egy kreatív megoldás, és matematikailag helyes, de nem használja fel az 'elvárt' tudást, nem számít helyesnek. (majd az igazi útmutatószövegben másképp lesz ez leírva)";
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
