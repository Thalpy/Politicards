using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardState : State
{
    public bool turnComplete;

    public bool crisisComplete;
    ActiveCrisis activeCrisis;
    public ActiveCrisis ActiveCrisis { get => activeCrisis; set => activeCrisis = value; }
    public Card Card { get => card; set => card = value; }
    Card card;


    public AwaitPlayerState awaitPlayerState;




    public override State RunCurrentState()
    { 
        PlayCard();
        if (turnComplete)
        {
            turnComplete = false;
            return awaitPlayerState;
        }

        return null;
    }

    void PlayCard()
    {
        // if not already playing card
        if (!turnComplete)
        {
            // set the card to be played
            activeCrisis.ApplyCard(card, activeCrisis.AICards.Length, false);
            // set the turn complete bool to true
            turnComplete = true;
        }
    }
}

