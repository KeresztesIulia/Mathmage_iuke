
using System;
using UnityEngine;

public class FloorCollider : MonoBehaviour
{

    [SerializeField] GameObject floorObjects;
    [SerializeField] int floorIndex;

    public static Action EnteredFloor3;
    public static Action LeftFloor3;

    private void OnTriggerEnter(Collider other)
    {
        PlayerControl player = other.GetComponent<PlayerControl>();
        if (player == null) player = other.GetComponentInParent<PlayerControl>();
        if (player == null) return;
        //floorObjects.SetActive(true);
        if (EnteredFloor3 != null && floorIndex == 3) EnteredFloor3();
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerControl player = other.GetComponent<PlayerControl>();
        if (player == null) player = other.GetComponentInParent<PlayerControl>();
        if (player == null) return;
        //floorObjects.SetActive(false);
        if (LeftFloor3 != null && floorIndex == 3) LeftFloor3();
    }
}
