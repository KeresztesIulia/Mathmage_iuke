using UnityEngine;

public class FLOOR35TELEPORTDELETE : MonoBehaviour
{
    [SerializeField] Vector3[] floorPositions;
    [SerializeField] Rigidbody player;

    int currentfloor = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CycleFloor();
            player.position = floorPositions[currentfloor];
        }
    }

    void CycleFloor()
    {
        currentfloor++;
        if (currentfloor > 5) currentfloor = 0;
    }
}
