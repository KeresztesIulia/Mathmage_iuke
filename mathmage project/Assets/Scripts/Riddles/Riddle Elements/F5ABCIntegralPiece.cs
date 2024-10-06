using System.Collections.Generic;
using UnityEngine;

public class F5ABCIntegralPiece : MonoBehaviour
{
    [SerializeField] F5ABCRiddle riddle;
    bool found = false;
    [SerializeField] int letter;
    
    public bool Found
    {
        get
        {
            return found;
        }
        set
        {
            if (value)
            {
                found = true;
                gameObject.SetActive(true);
                riddle.FoundPiece(letter);
            }
        }
    }

    public List<object> SaveData()
    {
        return new List<object>
        {
            found
        };
    }

    public void LoadData(List<object> data)
    {
        Found = (bool)data[0];
    }
}
