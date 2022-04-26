using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardState : State
{
    public bool turnComplete;
    
    public bool crisisComplete;

    public AwaitPlayerState awaitPlayerState;
    public override State RunCurrentState()
    {
        if (turnComplete)
        {   turnComplete = false;
            return awaitPlayerState;
        }

        return null;
    }

    //unity start function
}

