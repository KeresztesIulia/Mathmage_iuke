using System.Collections.Generic;
using UnityEngine;

public class TESTPHASEendgamePIN : MonoBehaviour, ISaveable
{
    [SerializeField] Closeable PINPanel;
    bool reachedEnd = false;

    private void OnTriggerEnter(Collider other)
    {
        if (GameHandler.GameInited && !reachedEnd)
        {
            GameFunctions.PlayerActive = false;
            PINPanel.gameObject.SetActive(true);
            reachedEnd = true;
        }
    }
    public List<object> SaveData()
    {
        return new List<object>
        {
            reachedEnd
        };
    }

    public void LoadData(List<object> data)
    {
        reachedEnd = (bool)data[0];
    }

}
