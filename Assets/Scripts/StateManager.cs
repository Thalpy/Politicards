using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] State currentState;

    protected CrisisExaminer crisisExaminer;

    Faction aiFaction;
    public Faction AiFaction { get => aiFaction; set => aiFaction = value; }

    Faction playerFaction;
    public Faction PlayerFaction { get => playerFaction; set => playerFaction = value; }

    int relationshipWithPlayer;
    public int RelationshipWithPlayer { get => relationshipWithPlayer; set => relationshipWithPlayer = value; }

    PlayerRelationshipEnum playerRelationship;
    public PlayerRelationshipEnum PlayerRelationship { get => playerRelationship; set => playerRelationship = value; }



    ActiveCrisis[] crises;



    void Update()
    {
        RunStateMachine();        
    }

    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            SwitchToNextState(nextState);
        }
    }


    private void SwitchToNextState(State nextState)
    {
        currentState = nextState;
    }

    public void PrepareExaminer()
    {
        crisisExaminer = new CrisisExaminer(GameMaster.crisisMaster.ActiveCrisses); // setup a new crisis examiner
    }
}
