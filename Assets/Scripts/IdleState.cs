using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI idle state
/// While in this state the AI simply listens for the signal that a crisis has started. 
/// once the crisis has started the AI switches to the await player state.
/// </summary>
public class IdleState : State
{
    public bool crisisStarted = false;
    
    public AwaitPlayerState awaitPlayerState;

    public override State RunCurrentState()
    {
        if (crisisStarted)
        {
            crisisStarted = false;
            return awaitPlayerState;
        }
        return null;
    }
}