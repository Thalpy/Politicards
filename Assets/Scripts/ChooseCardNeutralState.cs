using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The state the AI will be in when it is choosing a card and has a neutral relationship with the player
/// </summary>
public class ChooseCardNeutralState : State
{
    #region Fields

    /// <summary>
    /// true: the AI has chosen a card
    /// false: the AI has not chosen a card
    /// </summary>
    public bool cardChosen;

    /// <summary>
    /// true: the AI is choosing a card
    /// false: the AI has chosen a card
    /// </summary>
    public bool choosingCard;
    
    /// <summary>
    /// the crisis the AI has chosen
    /// </summary>
    ActiveCrisis activeCrisis;
    public ActiveCrisis ActiveCrisis { get => activeCrisis; set => activeCrisis = value; }
    StateManager stateManager = GameMaster.stateManager;

    /// <summary>
    /// the card the AI has chosen
    /// </summary>
    Card chosenCard;   

    /// <summary>
    /// the state the AI will move into once it has chosen a card
    /// </summary>
    public PlayCardState playCardState;    

    #endregion

    # region Methods

    /// <summary>
    /// handles the flow of execution of the state
    /// once the AI has chosen a card to play, it will move into the PlayCardState
    /// </summary>
    /// <returns>The next state</returns>
    public override State RunCurrentState()
    {
        if (cardChosen && !choosingCard)
        {
            cardChosen = false;
            playCardState.ActiveCrisis = activeCrisis;
            playCardState.Card = chosenCard;
            return playCardState;
        }
        if (!choosingCard && !cardChosen)
        {
            choosingCard = true;
            int aiFactionIndex = GameMaster.factionController.FactionDictionary[GameMaster.stateManager.AiFaction.FactionName];
            chosenCard = chooseCard(aiFactionIndex);
            choosingCard = false;
            cardChosen = true;
        }
        return null;
    }

    /// <summary>
    /// chooses a card to play
    /// In this case, the AI will choose the card whose Listed faction affiliation matches the AI's faction affiliation and has the highest value
    /// </summary>
    /// <returns>The card the AI has chosen</returns>
    Card chooseCard(int v)
    {
       //grab the cards in the ai hand
        Card[] cards = GameMaster.stateManager.GetCardsInHand().ToArray();

        //work out which card has the highest value for the faction the AI is affiliated with
        int highestValue = 0;
        int highestIndex = 0;
        for(int i = 0; i < cards.Length; i++)
        {
            Card card = cards[i];
            if (card.Faction == GameMaster.stateManager.AiFaction.FactionName && card.ProgressValues[v] >= highestValue && card.ManaCost <= GameMaster.stateManager.AiFaction.AIMana)
            {
                highestValue = card.ProgressValues[v];
                highestIndex = i;
            }
        }
        // if we found an appropriate card then return it
        if (highestValue > 0)
        {
            return cards[highestIndex];
        }
        
        // otherwise return a random card for the ai hand
        return cards[Random.Range(0, cards.Length)];
    }

    #endregion
}

