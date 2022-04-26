using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCardAllyState : State
{
    public bool cardChosen;

    ActiveCrisis activeCrisis;

    public PlayCardState playCardState;

    public ActiveCrisis ActiveCrisis { get => activeCrisis; set => activeCrisis = value; }

    public override State RunCurrentState()
    {
        if (cardChosen)
        {
            cardChosen = false;
            return playCardState;
        }
        return null;
    }

    // choose the card most likley to complete the current crisis

    Card chooseCard()
    {
        //grab the arrays of cards from the active crisis
        int numCards = activeCrisis.playerCards.Length + activeCrisis.AICards.Length;
        Card[] cards = new Card[numCards];
        //add the player cards to the array
        for (int i = 0; i < activeCrisis.playerCards.Length; i++)
        {
            cards[i] = activeCrisis.playerCards[i];
        }
        //add the AI cards to the array
        for (int i = activeCrisis.playerCards.Length; i < cards.Length; i++)
        {
            cards[i] = activeCrisis.AICards[i - activeCrisis.playerCards.Length];
        }
        //for each card in the array, get the ProgressValues
        List<int> progressValues = new List<int>();
        //add the ProgressValues from the first card to the list
        progressValues.AddRange(cards[0].ProgressValues);
        // for each remaining card in the array add the progress values to those already in the list
        for (int i = 1; i < cards.Length; i++)
        {
            //for each progress value in the progressValues list
            for (int j = 0; j < progressValues.Count; j++)
            {
                //add the progress value from the current card to the progress value in the list
                progressValues[j] += cards[i].ProgressValues[j];
            }
        }
        //find the index of the highest progress value
        int highestProgressValueIndex = 0;
        for (int i = 0; i < progressValues.Count; i++)
        {
            if (progressValues[i] > progressValues[highestProgressValueIndex])
            {
                highestProgressValueIndex = i;
            }
        }
        string targetFactionName;
        FactionEnum targetFactionEnum = (FactionEnum)highestProgressValueIndex;
        targetFactionName = targetFactionEnum.ToString();
        float aiMana = GameMaster.factionController.GetAiMana(targetFactionName);

        // select cards from the ais hand if they have the target faction and require less mana than the ai has to spend with that faction
        List<Card> targetCards = new List<Card>();
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].Faction == targetFactionName && cards[i].ManaCost <= aiMana)
            {
                targetCards.Add(cards[i]);
            }
           
        }
        // if there are no cards with the target faction, return a random card
        if (targetCards.Count == 0)
        {
            return cards[Random.Range(0, cards.Length)];
        }

        // select the card from target cards with the highest progress value for the target faction
        int highestProgressValueIndex2 = 0;
        for (int i = 0; i < targetCards.Count; i++)
        {
            if (targetCards[i].ProgressValues[highestProgressValueIndex] > targetCards[highestProgressValueIndex2].ProgressValues[highestProgressValueIndex])
            {
                highestProgressValueIndex2 = i;
            }
        }
        return targetCards[highestProgressValueIndex2];

    }
}

