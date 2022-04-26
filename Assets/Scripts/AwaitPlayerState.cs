using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwaitPlayerState : State
{
    public bool playerTurnComplete;

    public ChooseCardState chooseCardState;

    public override State RunCurrentState()
    {
        if (playerTurnComplete)
        {
            playerTurnComplete = false;
            return chooseCardState;
        }
        return null;
    }
}
