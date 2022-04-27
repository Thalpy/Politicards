using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwaitPlayerState : State
{
    public bool playerTurnComplete;

    StateManager stateManager = GameMaster.stateManager;

    CrisisMaster crisisMaster = GameMaster.crisisMaster;

    [SerializeField] ChooseCrisisAllyState chooseCrisisState;

    [SerializeField] ChooseCrisisEnemyState chooseCrisisEnemyState;

    [SerializeField] ChooseCrisisNeutralState chooseCrisisNeutralState;

    public override State RunCurrentState()
    {
        // reset the choose crisis states
        chooseCrisisState.ChosenCrisis = null;
        chooseCrisisEnemyState.ChosenCrisis = null;
        chooseCrisisNeutralState.ChosenCrisis = null;

        if (playerTurnComplete && stateManager.RelationshipWithPlayer > 0.5)
        {
            playerTurnComplete = false;
            return chooseCrisisState;
        }
        if (playerTurnComplete && stateManager.RelationshipWithPlayer >= 0.25 && stateManager.RelationshipWithPlayer <= 0.5)
        {
            playerTurnComplete = false;
            return chooseCrisisNeutralState;
        }
        if (playerTurnComplete && stateManager.RelationshipWithPlayer < 0.25)
        {
            playerTurnComplete = false;
            return chooseCrisisEnemyState;
        }
        return null;
    }

    public void SetPlayerTurnComplete()
    {
        playerTurnComplete = true;
    }


    void Awake()
    {
        // subscribe to the player turn complete event on the crisis master
        crisisMaster.PlayerPlayedCardEvent.AddListener(SetPlayerTurnComplete);
    }

    
}
