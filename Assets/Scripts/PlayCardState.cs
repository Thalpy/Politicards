using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardState : State
{
    public bool turnComplete;
    
    public bool crisisComplete;

    public AwaitPlayerState awaitPlayerState;

    public IdleState idleState;
    public override State RunCurrentState()
    {
        if (turnComplete)
        {   turnComplete = false;
            return awaitPlayerState;
        }
        if (crisisComplete)
        {
            turnComplete = false;
            crisisComplete = false;
            return idleState;
        }
        return null;
    }
}

