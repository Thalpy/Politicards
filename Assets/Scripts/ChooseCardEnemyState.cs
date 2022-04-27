using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCardEnemyState : State
{
    public bool cardChosen;

    public bool choosingCard;

    Card chosenCard;

    ActiveCrisis activeCrisis;

    public PlayCardState playCardState;

    public ActiveCrisis ActiveCrisis { get => activeCrisis; set => activeCrisis = value; }

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
            chosenCard = chooseCard();
        }
        return null;
    }

    // choose the card most likley to complete the current crisis

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
            cardProgressValues.Add(card.GetComponent<Card>().ProgressValues);
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
        return cardObjects[indexOfLowestValue].GetComponent<Card>();



    }
}


