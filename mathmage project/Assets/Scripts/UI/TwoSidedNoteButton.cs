using UnityEngine;

public class TwoSidedNoteButton : MonoBehaviour
{
    [SerializeField] GameObject otherSide;

    public void ChangeSide()
    {
        otherSide.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }   
}
