using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    /// <summary>
    /// Enum that holds the support objectives that an Ai can work on
    /// The AI will seek to build support with this faction when making card choices
    /// </summary>
    public enum AISupportObjectiveEnum
    {
        MilitarySupport,
        EconomicSupport,
        PeopleSupport,
        NobilitySupport,
        CrimeSupport,
        PlayerSupport,
        PlayerAttack,

}

    /// <summary>
    /// Enum that holds the possible sentiments that an Ai can have towards the player
    /// The AI may use this to determine how it will react to the player
    /// </summary>
    public enum AIPlayerSentimentEnum
    {
        Neutral,
        Positive,
        Negative
    }

// a reference to the current AI hand
    GameObject AIHand;

// a reference to the current player card deck
    GameObject playerHand;

    // enum fields with getters and setters
    [SerializeField] private AISupportObjectiveEnum aISupportObjective;
    public AISupportObjectiveEnum AISupportObjective
    {
        get { return aISupportObjective; }
        set { aISupportObjective = value;
            //support objective has changed- call function to process effect here
        }
    }

    [SerializeField] private AIPlayerSentimentEnum aiPlayerSentiment;
    public AIPlayerSentimentEnum AIPlayerSentiment
    {
        get { return aiPlayerSentiment; }
        set { aiPlayerSentiment = value;
            //sentiment has changed- call function to process effect here
            }
        }

    //a private integer representing the current relationship with the player (0-100) with getters and setters
    private int relationship;
    public int Relationship
    {
        get { return relationship; }
        set { // if the relationship is less than 0, set it to 0
            if (value < 0)
            {
                relationship = 0;
            }
            // if the relationship is greater than 100, set it to 100
            else if (value > 100)
            {
                relationship = 100;
            }
            // otherwise, set the relationship to the value passed in
            else
            {
                relationship = value;
            }
            //call the ChangeSentiment function to process the new relationship value
            ChangeSentiment();
        }
    }

    //a function to change the AIPlayerSentiment based on the relationship
    public void ChangeSentiment()
    {
        // if the relationship is less than 25, set the AIPlayerSentiment to negative
        if (relationship < 25)
        {
            //if the AIPPlayerSentiment is already negative, do nothing
            if (aiPlayerSentiment == AIPlayerSentimentEnum.Negative)
            {
                return;
            }
            AIPlayerSentiment = AIPlayerSentimentEnum.Negative;
        }
        // if the relationship is between 25 and 50, set the AIPlayerSentiment to neutral
        else if (relationship >= 25 && relationship <= 50)
        {
            //if the AIPPlayerSentiment is already neutral, do nothing
            if (aiPlayerSentiment == AIPlayerSentimentEnum.Neutral)
            {
                return;
            }
            AIPlayerSentiment = AIPlayerSentimentEnum.Neutral;
        }
        // if the relationship is greater than 50, set the AIPlayerSentiment to positive
        else
        {
            //if the AIPPlayerSentiment is already positive, do nothing
            if (aiPlayerSentiment == AIPlayerSentimentEnum.Positive)
            {
                return;
            }
            AIPlayerSentiment = AIPlayerSentimentEnum.Positive;
        }
    }

    // a function to decide if a card can be played which takes in a card and returns a boolean
    public bool CanPlayCard(Card card)
    {
        // if the mana cost of the card is greater than the faction specific mana pool, return false. Get the faction name from the card faction
        if (card.ManaCost > GameMaster.GetFactionMana(card.Faction))
        {
            return false;
        }

        // if the AIPlayerSentiment is negative, return false if the card is a support card
        if (aiPlayerSentiment == AIPlayerSentimentEnum.Negative && card.Faction == "Support")
        {
            return false;
        }

        // if the AIPlayerSentiment is positive, return false if the card is an attack card
        if (aiPlayerSentiment == AIPlayerSentimentEnum.Positive && card.Faction == "Attack")
        {
            return false;
        }

        // if the card is a support card and the AIPlayerSentiment is neutral, return false
        if (card.Faction == "Support" && aiPlayerSentiment == AIPlayerSentimentEnum.Neutral)
        {
            return false;
        }

        // if the card is an attack card and the AIPlayerSentiment is neutral, return false
        if (card.Faction == "Attack" && aiPlayerSentiment == AIPlayerSentimentEnum.Neutral)
        {
            return false;
        }

        // if the card is an attack card and the AIPlayerSentiment is positive, return false
        if (card.Faction == "Attack" && aiPlayerSentiment == AIPlayerSentimentEnum.Positive)
        {
            return false;
        }

        // if the card is a support card and the AIPlayerSentiment is negative, return false
        if (card.Faction == "Support" && aiPlayerSentiment == AIPlayerSentimentEnum.Negative)
        {
            return false;
        }

        // if the cards faction is Military, return false if the AI does not have military support and return true if the AI does have military support and the card increases the crisis progress of the military faction
        if (card.Faction == "Military" && aISupportObjective != AISupportObjectiveEnum.MilitarySupport && card.MilitarySupportIncrease > 0)
        {
            return false;
        }

        // if the cards faction is Economic, return false if the AI does not have economic support and return true if the AI does have economic support and the card increases the crisis progress of the economic faction
        if (card.Faction == "Economic" && aISupportObjective != AISupportObjectiveEnum.EconomicSupport && card.EconomicSupportIncrease > 0)
        {
            return false;
        }

        // if the cards faction is People, return false if the AI does not have people support and return true if the AI does have people support and the card increases the crisis progress of the people faction
        if (card.Faction == "People" && aISupportObjective != AISupportObjectiveEnum.PeopleSupport && card.PeopleSupportIncrease > 0)
        {
            return false;
        }

        // if the cards faction is Nobility, return false if the AI does not have nobility support and return true if the AI does have nobility support and the card increases the crisis progress of the nobility faction
        if (card.Faction == "Nobility" && aISupportObjective != AISupportObjectiveEnum.NobilitySupport && card.NobilitySupportIncrease > 0)
        {
            return false;
        }
        
    }

    //a function which takes in a list of cards and returns a list of cards ordered by the cards progress increase
    // pass this function a list of cards whose faction match the AI's support objective
    public List<Card> OrderCardsByProgressIncrease(List<Card> cards)
    {
        //create a new list of cards
        List<Card> orderedCards = new List<Card>();

        //loop through the cards in the list of cards
        for (int i = 0; i < cards.Count; i++)
        {
            //create a new variable to hold the index of the card with the highest progress increase
            int highestIndex = 0;

            //loop through the cards in the list of cards
            for (int j = 0; j < cards.Count; j++)
            {
                //if the card at the current index has a higher progress increase than the card at the highest index, set the highest index to the current index
                if (cards[j].ProgressIncrease > cards[highestIndex].ProgressIncrease)
                {
                    highestIndex = j;
                }
            }

            //add the card at the highest index to the ordered cards list
            orderedCards.Add(cards[highestIndex]);

            //remove the card at the highest index from the cards list
            cards.RemoveAt(highestIndex);
        }

        //return the ordered cards list
        return orderedCards;
    }

    //a function which takes in a list of cards and returns a list of cards ordered by the cards faction power increase
    // pass this function a list of cards whose faction match the AI's support objective
    public List<Card> OrderCardsByFactionPowerIncrease(List<Card> cards)
    {
        //create a new list of cards
        List<Card> orderedCards = new List<Card>();

        //loop through the cards in the list of cards
        for (int i = 0; i < cards.Count; i++)
        {
            //create a new variable to hold the index of the card with the highest faction power increase
            int highestIndex = 0;

            //loop through the cards in the list of cards
            for (int j = 0; j < cards.Count; j++)
            {
                //if the card at the current index has a higher faction power increase than the card at the highest index, set the highest index to the current index
                if (cards[j].FactionPowerIncrease > cards[highestIndex].FactionPowerIncrease)
                {
                    highestIndex = j;
                }
            }

            //add the card at the highest index to the ordered cards list
            orderedCards.Add(cards[highestIndex]);

            //remove the card at the highest index from the cards list
            cards.RemoveAt(highestIndex);
        }

        //return the ordered cards list
        return orderedCards;
    }





    
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
