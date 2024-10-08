using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Doors that can't be opened by themselves, only if given riddles are solved
/// </summary>

public class RiddleDoor : Door
{
    [SerializeField] protected Riddle[] requisites;

    public bool openable
    {
        get
        {
            if (requisites == null)
            {
                return true;
            }
            else
            {
                foreach (Riddle riddle in requisites)
                {
                    if (!riddle.Solved) return false;
                }
                return true;
            }
        }
    }

    protected override void Update()
    {
        if (openable)
        {
            base.Update();
        }

    }

    public override void Activate()
    {
        if (openable)
        {
            base.Activate();
        }
    }
}


