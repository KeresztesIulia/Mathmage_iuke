using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Doors that can't be opened by themselves, only if given riddles are solved
/// </summary>

// might want to keep track of whether it was already deemed openable before, because if it was, then it really needs
// no rechecking every fricking frame for like... 5 doors or whatever.
// would also help with the one skippable floor -> I could set that "did I say it's openable before" variable to true
// but I am not making it public. Maybe an action the script reacts to, although that would then happen to every RiddleDoor
// so I have to specialize it somehow

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


