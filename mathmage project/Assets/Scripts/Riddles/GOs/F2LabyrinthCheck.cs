using System.Collections.Generic;
using UnityEngine;

// check to see that people didn't just glitch throught the labyrinth walls to reach the end
public class F2LabyrinthCheck : MonoBehaviour, ISaveable
{
    // null between two routes
    [SerializeField] F2LabyrinthDoorRiddle[] routes;

    [SerializeField] F2GlitchMessage glitchedMessage;
    [SerializeField] Vector3 startPosition;
    bool awaitResponse = false;
    bool passed = false;

    bool SolvedAnyRoute()
    {
        bool solvedARoute = true;
        foreach (F2LabyrinthDoorRiddle door in routes)
        {
            if (door == null)
            {
                if (solvedARoute) return true;
                else solvedARoute = true;
            }
            else
            {
                if (!solvedARoute) continue;
                solvedARoute &= door.Solved;
            }
        }
        return solvedARoute;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameHandler.GameInited && !passed)
            if (!SolvedAnyRoute())
            {
                GameFunctions.PlayerActive = false;
                glitchedMessage.gameObject.SetActive(true);
                awaitResponse = true;
            }
            else passed = true;
    }

    private void Update()
    {
        if (awaitResponse)
        {
            if (glitchedMessage.Response != null)
            {
                awaitResponse = false;
                PlayerControl player = FindObjectOfType<PlayerControl>();
                if (player != null)
                {
                    player.transform.position = startPosition;
                }
                
            }
        }
    }

    public List<object> SaveData()
    {
        return new List<object>
        {
            passed
        };
    }

    public void LoadData(List<object> data)
    {
        passed = (bool)data[0];
    }
}
