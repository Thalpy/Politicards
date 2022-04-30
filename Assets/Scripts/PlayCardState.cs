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

    bool attemptedToPlay;

    public bool AttemptedToPlay {get => attemptedToPlay; set => attemptedToPlay = value; }

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
        if(attemptedToPlay)
        {
            awaitPlayerState.PlayerTurnComplete = false;
            return awaitPlayerState;
        }
        if (!turnComplete)
        {
            attemptedToPlay = true;
            PlayCard();
            turnComplete = true;
        }
        if (turnComplete)
        {
            turnComplete = false;
            //make sure that the player turn complete bool is set to false to avoid looping.
            awaitPlayerState.PlayerTurnComplete = false;
            return awaitPlayerState;
        }

        return null;
    }

    /// <summary>
    /// plays the card the AI has chosen
    /// </summary>
    void PlayCard()
    {
        // if not already playing card
        if (!turnComplete)
        {
            // set the card to be played
            TargetCrisis tgt = activeCrisis. GetTargetFromIndex(getFirstEmptyAICardSlot());

            tgt.DropCardAI(card);

            //activeCrisis.ApplyCard(card, getFirstEmptyAICardSlot(), false);
            // set the turn complete bool to true
            turnComplete = true;
        }

        // a helper function to find the first slot on the crisis for the AI
        int getFirstEmptyAICardSlot()
        {
            for (int i = 0; i < activeCrisis.AICards.Length; i++)
            {
                if (activeCrisis.AICards[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}

