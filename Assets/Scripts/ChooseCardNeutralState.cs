using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCardNeutralState : State
{
    public bool cardChosen;

    public bool choosingCard;

    StateManager stateManager = GameMaster.stateManager;

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

    // choose the card which corresponds to the AIs faction

    Card chooseCard()
    {
       //grab the cards in the ai hand
        List<GameObject> cardObjects = GameMaster.AISHand.CardsInHand;
        foreach(GameObject cardObject in cardObjects)
        {
            Card card = cardObject.GetComponent<Card>();
            if (card.Faction == stateManager.AiFaction.FactionName)
            {
                return card;
            }
        }
        // otherwise return a random card for the ai hand
        return cardObjects[Random.Range(0, cardObjects.Count)].GetComponent<Card>();
    }
}

