using System.Collections.Generic;
using UnityEngine;

public class F5ABCIntegralPieceGO : MonoBehaviour, IActivatable, ISaveable
{
    [SerializeField] F5ABCIntegralPiece piece;
    bool found = false;

    public string ActivationText
    {
        get { return "???"; }
    }

    public string Name { get { return "???"; } }

    public void Activate()
    {
        piece.Found = true;
        gameObject.SetActive(false);
        found = true;
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
        found = (bool)data[0];
        if (found)
        {
            gameObject.SetActive(false);
            piece.Found = true;
        }
        
    }

 

   
}
