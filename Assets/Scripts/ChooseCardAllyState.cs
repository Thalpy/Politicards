using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The state the AI will be in when choosing a card to play if it has a good relationship with the player
/// </summary>
public class ChooseCardAllyState : State
{
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
    /// the card the AI has chosen
    /// </summary>
    Card chosenCard;

    /// <summary>
    /// The crisis the AI has chosen to play
    /// </summary>
    ActiveCrisis activeCrisis;

    /// <summary>
    /// The state the AI will move into once it has chosen a card
    /// </summary>
    public PlayCardState playCardState;

    public ActiveCrisis ActiveCrisis { get => activeCrisis; set => activeCrisis = value; }

    /// <summary>
    /// handles the flow of execution of the state
    /// once the AI has chosen a card to play, it will move into the PlayCardState
    /// </summary>
    public override State RunCurrentState()
    {
        if (cardChosen && !choosingCard)
        {
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
            choosingCard = false;
        }
        return null;
    }

    
    /// <summary>
    /// Chooses the card which is most likley to lead to a positive outcome for the current crisis
    /// </summary>
    /// <returns>The card which is most likley to lead to a positive outcome for the current crisis</returns>
    Card chooseCard()
    {
        //get the cards in the AI's hand
        int numCards = activeCrisis.playerCards.Length + activeCrisis.AICards.Length;
        Card[] cards = GameMaster.stateManager.GetCardsInHand().ToArray();

        //get the faction on the crisis that has the highest progress
        Dictionary<Faction, int> progress = activeCrisis.crisis.factionProgress;
        Faction targetFaction = GetHighestProgressFaction(progress);

        // select cards from the ais hand if they have the same faction as the target faction and require less mana than the ai has to spend with that faction
        List<Card> targetCards = getTargetCards(cards, targetFaction.FactionName);

        // if there are no cards with the target faction, return a random card
        if (targetCards.Count == 0)
        {
            return cards[Random.Range(0, cards.Length)];
        }

        //otherwise return the card with the highest progress for the target faction
        return getHighestProgressCard(targetCards, targetFaction);

    }

    /// <summary>
    /// Gets the card with the highest progress value for the given faction
    /// </summary>
    /// <param name="targetCards">The cards to search through</param>
    /// <param name="targetFaction">The faction to search for</param>
    /// <returns>The card with the highest progress value for the given faction</returns>
    private Card getHighestProgressCard(List<Card> targetCards, Faction targetFaction)
    {
        int highestProgressValueIndex = 0;
        int highestProgressValue = 0;
        for (int i = 0; i < targetCards.Count; i++)
        {
            int power = targetCards[i].ProgressValues[GameMaster.factionController.FactionDictionary[targetFaction.FactionName]];
            if (power > highestProgressValue)
            {
                highestProgressValue = power;
                highestProgressValueIndex = i;
            }
        }
        return targetCards[highestProgressValueIndex];
        
    }

    /// <summary>
    /// Gets the cards from the AI's hand that have the target faction and require less mana than the AI has to spend with that faction
    /// </summary>
    /// <param name="cards">The array of cards to search through</param>
    /// <param name="targetFactionName">The name of the target faction</param>
    /// <returns>A list of cards that have the target faction and require less mana than the AI has to spend with that faction</returns>
    private List<Card> getTargetCards(Card[] cards, string targetFactionName)
    {
        List<Card> targetCards = new List<Card>();
        for (int i = 0; i < cards.Length; i++)
        {
            if (cardIsOfCorrectFactionAndPlayable(cards, targetFactionName, i))
            {
                targetCards.Add(cards[i]);
            }

        }

        return targetCards;
    }

    /// <summary>
    /// Checks if the card is of the correct faction and is playable
    /// </summary>
    /// <param name="cards">The array of cards to search through</param>
    /// <param name="targetFactionName">The name of the target faction</param>
    /// <param name="index">The index of the card to check</param>
    /// <returns>true if the card is of the correct faction and is playable</returns>
    private bool cardIsOfCorrectFactionAndPlayable(Card[] cards, string targetFactionName, int i)
    {
        return cards[i].Faction == targetFactionName && cards[i].ManaCost <= (float)GameMaster.factionController.GetAiMana(targetFactionName);
    }

    /// <summary>
    /// Gets the faction with the highest progress on the crisis
    /// </summary>
    /// <param name="progress">The dictionary of factions and their progress on the crisis</param>
    /// <returns>The faction with the highest progress on the crisis</returns>
    private Faction GetHighestProgressFaction(Dictionary<Faction, int> progress)
    {
        int highestValue = 0;
        Faction highestFaction = null;
        foreach (KeyValuePair<Faction, int> pair in progress)
        {
            if (pair.Value > highestValue)
            {
                highestValue = pair.Value;
                highestFaction = pair.Key;
            }
        }
        return highestFaction;
    }
}

