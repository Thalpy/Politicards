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

    
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
