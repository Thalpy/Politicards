using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The State machine controller for the AI. Basically the AI's brain.
/// </summary>
public class StateManager : MonoBehaviour
{
    #region Fields

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
    public int RelationshipWithPlayer { get => relationshipWithPlayer; set => relationshipWithPlayer = value; }

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
        RunStateMachine();        
    }

    void Awake()
    {
        //subscribe to the crisis master's player played card event so that we update our guess at the player faction for each round played.
        GameMaster.crisisMaster.PlayerPlayedCardEvent.AddListener(GetPlayerFaction);
    }

    /// <summary>
    /// Sets the player faction to the faction that is happiest with the player
    /// </summary>
    public void GetPlayerFaction()
    {
        float highestHappiness = 0;
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
    }

    /// <summary>
    /// Runs the current state of the state machine
    /// </summary>
    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState(); // returns null if we should continue with the current state otherwise returns the next state

        if (nextState != null)
        {
            SwitchToNextState(nextState);
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
}
