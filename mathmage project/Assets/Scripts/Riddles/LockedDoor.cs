using UnityEngine;

public class LockedDoor : RiddleDoor
{
    [SerializeField] GameObject[] locks;

    protected override void Start()
    {
        if (requisites == null || requisites.Length == 0) throw new System.Exception("A LockedDoor with no requisites is just a Door!");
        if (requisites.Length != locks.Length) throw new System.Exception("Requisite and lock count don't match!");

        base.Start();

    }

    protected override void Update()
    {
        base.Update();
        ChangeLocks();
    }

    void ChangeLocks()
    {
        for (int i = 0; i < requisites.Length; i++) locks[i].SetActive(!requisites[i].Solved);
    }


}
