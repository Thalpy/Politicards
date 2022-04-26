using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCardState : State
{
    public bool cardChosen;

    public PlayCardState playCardState;
    public override State RunCurrentState()
    {
        if (cardChosen)
        {
            cardChosen = false;
            return playCardState;
        }
        return null;
    }
}

