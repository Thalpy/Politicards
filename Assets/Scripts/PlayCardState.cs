using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardState : State
{
    #region instance variables
    /// <summary>
    /// true: the AI has played a card
    /// false: the AI has not played a card
    /// </summary>
    public bool turnComplete;

    /// <summary>
    /// the current crisis the AI is playing
    /// </summary>
    ActiveCrisis activeCrisis;
    public ActiveCrisis ActiveCrisis { get => activeCrisis; set => activeCrisis = value; }

    /// <summary>
    /// the card the AI has chosen to play
    /// </summary>
    Card card;
    public Card Card { get => card; set => card = value; }
    


    public AwaitPlayerState awaitPlayerState;

    #endregion

    /// <summary>
    /// handles the flow of execution of the state
    /// once the AI has played a card, it will move into the AwaitPlayerState
    /// </summary>
    /// <returns>The next state</returns>
    public override State RunCurrentState()
    { 
        PlayCard();
        if (turnComplete)
        {
            turnComplete = false;
            awaitPlayerState.playerTurnComplete = false;
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

