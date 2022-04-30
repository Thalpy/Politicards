using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The state the AI will be in when it is choosing a crisis and has a good relationship with the player
/// </summary>
public class ChooseCardEnemyState : State
{   
    #region Fields

    /// <summary>
    /// true: the AI has chosen a crisis
    /// false: the AI has not chosen a crisis
    /// </summary>
    public bool cardChosen;

    /// <summary>
    /// true: the AI is choosing a crisis
    /// false: the AI has chosen a crisis
    /// </summary>
    public bool choosingCard;

    /// <summary>
    /// the card the AI has chosen 
    /// </summary>
    public Card chosenCard;

    /// <summary>
    /// The crisis the AI has chosen
    /// </summary>
    ActiveCrisis activeCrisis;
    public ActiveCrisis ActiveCrisis { get => activeCrisis; set => activeCrisis = value; }

    /// <summary>
    /// The state the AI will move into once it has chosen a crisis
    /// </summary>
    public PlayCardState playCardState;   

    #endregion

    /// <summary>
    /// handles the flow of execution of the state
    /// once the AI has chosen a crisis to play, it will move into the PlayCardState
    /// </summary>
    public override State RunCurrentState()
    {
        playCardState.ActiveCrisis = null;
        playCardState.Card = null;
        playCardState.AttemptedToPlay = false;
        playCardState.turnComplete = false;
        if (cardChosen && !choosingCard)
        {
            Debug.Log("Card Chosen: " + chosenCard.Name);
            cardChosen = false;
            playCardState.ActiveCrisis = activeCrisis;
            playCardState.AttemptedToPlay = false;
            playCardState.Card = chosenCard;
            return playCardState;
        }
        if (!choosingCard && !cardChosen)
        {
            choosingCard = true;
            chosenCard = chooseCard();
            cardChosen = true;
        }
        return null;
    }

    /// <summary>
    /// chooses a crisis to play.
    /// in this case the AI will choose the crisis that is least likley to increase the faction progress of the player faction.
    /// </summary>
    Card chooseCard()
    {
        List<GameObject> cardObjects = GameMaster.AISHand.CardsInHand;

        string playerFaction = GameMaster.stateManager.PlayerFaction.FactionName;
        int playerFactionIndex = GameMaster.factionController.FactionDictionary[playerFaction];

        //switch statement to determine the index of the playerFaction

        List<List<int>> cardProgressValues = new List<List<int>>();

        // for each card in the ai hand get the list of progress values
        foreach (GameObject card in cardObjects)
        {
            cardProgressValues.Add(card.GetComponent<JL_CardController>()._Card.ProgressValues);
            //cardProgressValues.Add(card.GetComponent<Card>().ProgressValues);
        }

        int numberOfCardsInHand = cardProgressValues.Count;

        // for each list of progress values in cardProgressValues find the lowest value of the element at the playerFactionIndex
        int lowestValue = int.MaxValue;
        int indexOfLowestValue = 0;
        for (int i = 0; i < numberOfCardsInHand; i++)
        {
            int value = cardProgressValues[i][playerFactionIndex];
            if (value < lowestValue)
            {
                lowestValue = value;
                indexOfLowestValue = i;
            }
        }

        // return the card at the index of the lowest value
        choosingCard = false;
        return cardObjects[indexOfLowestValue].GetComponent<JL_CardController>()._Card;


    }
}


