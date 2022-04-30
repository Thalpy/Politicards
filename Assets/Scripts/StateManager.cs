using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The State machine controller for the AI. Basically the AI's brain.
/// </summary>
public class StateManager : MonoBehaviour
{
    #region Fields

    bool recovering;

    [SerializeField] GameObject rootNode;



    /// <summary>
    /// The current state the AI is in.
    /// </summary>
    [SerializeField] State currentState;

    /// <summary>
    /// deprecated. old object that analyzed a crisis to see if it is worth playing
    /// </summary>
    protected CrisisExaminer crisisExaminer;

    /// <summary>
    /// The faction the AI wants to support
    /// </summary>
    Faction aiFaction;
    public Faction AiFaction { get => aiFaction; set => aiFaction = value; }

    /// <summary>
    /// The faction that's happiest with the player (and therefore rewards the most mana to the player)
    Faction playerFaction;
    public Faction PlayerFaction { get => playerFaction; set => playerFaction = value; }

    /// <summary>
    /// the relationship between the player and the AI
    /// </summary>
    int relationshipWithPlayer;
    public int RelationshipWithPlayer
    {
        get
        {
            return relationshipWithPlayer;
        }
        set
        {
            switch (value)
            {
                case > 10:
                    relationshipWithPlayer = 10;
                    break;
                case < 0:
                    relationshipWithPlayer = 0;
                    break;
                default:
                    relationshipWithPlayer = value;
                    break;
            }
        }
    }
    /// <summary>
    /// An enum representing the top level relationship statuses between the AI and the player
    /// </summary>
    PlayerRelationshipEnum playerRelationship;
    public PlayerRelationshipEnum PlayerRelationship { get => playerRelationship; set => playerRelationship = value; }

    /// <summary>
    /// ActiveCrises - possibly deprecated 
    /// TODO: remove this
    /// </summary>
    ActiveCrisis[] crises;

    #endregion


    /// <summary>
    /// Initializes the AI's state machine
    /// </summary>
    void Update()
    {
        if(recovering == true)
        {
            recovering = false;
            gameObject.SetActive(true);
        }
        RunStateMachine();
    }


    void Start()
    {
        //subscribe to the crisis master's player played card event so that we update our guess at the player faction for each round played.
        GameMaster.crisisMaster.PlayerPlayedCardEvent.AddListener(GetPlayerFaction);
    #if AI_PLAYER_RELATIONSHIP_NEUTRAL
        playerRelationship = PlayerRelationshipEnum.Neutral;
        RelationshipWithPlayer = 5;
    #elif AI_PLAYER_RELATIONSHIP_HAPPY
                playerRelationship = PlayerRelationshipEnum.Ally;
                RelationshipWithPlayer = 10;
    #elif AI_PLAYER_RELATIONSHIP_UNHAPPY
                playerRelationship = PlayerRelationshipEnum.Enemy;
                RelationshipWithPlayer = 1;
    #endif
    #if AI_FACTION_RANDOM
        List<Faction> factions = GameMaster.factionController.GetFactions();
        int randomIndex = Random.Range(0, factions.Count);
        AiFaction = factions[randomIndex];
        //get a random number between 1 -5 
        int randomRelationship = Random.Range(1, 6);
        foreach (Card card in GameMaster.cardMaster.Decks[randomRelationship].cards)
        {
            GameMaster.AISHand.AddCard(card);
        }
    #endif

        GetPlayerFaction();

    }

    /// <summary>
    /// Sets the player faction to the faction that is happiest with the player
    /// </summary>
    public void GetPlayerFaction()
    {
        float highestHappiness = -10; // make sure this is less than 0 to start
        Faction happiestFaction = null;
        List<Faction> factionList = GameMaster.factionController.GetFactions();
        foreach (Faction faction in factionList)
        {
            if (faction.FactionHappiness > highestHappiness)
            {
                highestHappiness = faction.FactionHappiness;
                happiestFaction = faction;
            }
        }
        PlayerFaction = happiestFaction;
    }

    /// <summary>
    /// Runs the current state of the state machine
    /// </summary>
    private void RunStateMachine()
    {
        try{

        
        State nextState = currentState?.RunCurrentState(); // returns null if we should continue with the current state otherwise returns the next state

        if (nextState != null)
        {
            SwitchToNextState(nextState);
        }
        }
        catch(System.Exception e)
        {
            recovering = true;
            Debug.LogError(e.Message);
            Debug.LogError("Something has occured and the AI broke :(");
            Debug.LogError("The AI will try and recover from this error");
            Debug.LogError("If you've got here the AI totally shit the bed, this is not good");
            gameObject.SetActive(false);
            recoverAISubsystems();

        }
    }

    /// <summary>
    /// Switches the current state to the next state
    /// </summary>
    /// <param name="nextState">The next state to switch to</param>
    private void SwitchToNextState(State nextState)
    {
        currentState = nextState;
    }

    /// <summary>
    /// Deprecated
    /// TODO: remove this
    /// </summary>
    public void PrepareExaminer()
    {
        crisisExaminer = new CrisisExaminer(GameMaster.crisisMaster.ActiveCrisses); // setup a new crisis examiner
    }

    /// <summary>
    /// Converts the RelationshipWithPlayer to a PlayerRelationshipEnum
    /// TODO: consider removing and doing away with the enum - straight use of the int is probably easier to grok
    /// </summary>
    /// <returns>The PlayerRelationshipEnum</returns>

    public PlayerRelationshipEnum GetPlayerRelationship()
    {
        if (RelationshipWithPlayer > 5)
        {
            return PlayerRelationshipEnum.Ally;
        }
        else if (RelationshipWithPlayer < 3)
        {
            return PlayerRelationshipEnum.Enemy;
        }
        else
        {
            return PlayerRelationshipEnum.Neutral;
        }
    }

    public List<Card> GetCardsInHand()
    {
        List<GameObject> cardObjects = GameMaster.AISHand.CardsInHand;
        List<Card> cards = new List<Card>();
        foreach (GameObject cardObject in cardObjects)
        {
            cards.Add(cardObject.GetComponent<JL_CardController>()._Card);
        }
        return cards;
    }

    /// <summary>
    /// Given a string, sets the current relationship between the player and the AI to the appropriate value. 
    /// </summary>
    /// <param name="relationship">
    ///The relationship between the player and the AI
    ///Valid values are: "Ally", "Enemy", "Neutral" 
    ///</param>
    public void SetRelationshipByString(string relationship)
    {
        switch (relationship)
        {
            case "Ally":
                playerRelationship = PlayerRelationshipEnum.Ally;
                RelationshipWithPlayer = Random.Range(5, 11);
                break;
            case "Enemy":
                playerRelationship = PlayerRelationshipEnum.Enemy;
                RelationshipWithPlayer = Random.Range(0, 3);
                break;
            case "Neutral":
                playerRelationship = PlayerRelationshipEnum.Neutral;
                RelationshipWithPlayer = Random.Range(3, 5);
                break;
        }
    }

    /// <summary>
    /// tries to rebuild the AI system following a collapse of the thread running the AI
    /// </summary>
    void recoverAISubsystems()
    {
        DestroyImmediate(rootNode);
        //load the ai prefab
        GameObject ai = Instantiate(Resources.Load("AI")) as GameObject;
        rootNode = ai;
        currentState = rootNode.transform.GetChild(0).GetComponent<AwaitPlayerState>();
    }

}
