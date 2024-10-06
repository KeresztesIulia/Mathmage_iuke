using TMPro;
using UnityEngine;

public class draggableTestScript : MonoBehaviour
{
    [SerializeField] DropSpot ds;
    [SerializeField] TextMeshProUGUI text;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) text.text = ds.actualValue;   
    }
}
