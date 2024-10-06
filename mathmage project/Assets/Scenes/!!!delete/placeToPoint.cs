
using UnityEngine;

public class placeToPoint : MonoBehaviour
{
    [SerializeField] RectTransform toPlace;
    [SerializeField] RectTransform where;
    [SerializeField] Canvas parentCanvas;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && toPlace != null && where != null)
        {
            toPlace.position = GameFunctions.SamePlace(toPlace, where, parentCanvas);

        }
    }
}
