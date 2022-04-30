using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In this state the AI will wait to recieve the PlayerPlayedCard event
/// after receiving this event, the AI will move into one of three crisis choosing states based on the player's relationship with the AI
/// </summary>
public class AwaitPlayerState : State
{
    /// <summary>
    /// true: the AI has recieved the PlayerPlayedCard event
    /// false: the AI has not recieved the PlayerPlayedCard event
    /// </summary>

    bool playerTurnComplete;
    public bool PlayerTurnComplete { get => playerTurnComplete; set => playerTurnComplete = value; }
    StateManager stateManager;

    CrisisMaster crisisMaster;

    /// <summary>
    /// the state the AI will move into once the PlayerPlayedCard event has been recieved if the player has a good relationship with the AI
    /// </summary>
    [SerializeField] public ChooseCrisisAllyState chooseCrisisState;

    /// <summary>
    /// the state the AI will move into once the PlayerPlayedCard event has been recieved if the player has a bad relationship with the AI
    /// </summary>
    [SerializeField] public ChooseCrisisEnemyState chooseCrisisEnemyState;

    /// <summary>
    /// the state the AI will move into once the PlayerPlayedCard event has been recieved if the player has a neutral relationship with the AI
    /// </summary>
    [SerializeField] public ChooseCrisisNeutralState chooseCrisisNeutralState;

    /// <summary>
    /// handles the logic for determining which crisis state the AI will move into
    /// makes sure that the choose crisis states do not have a chosen crisis before moving into them
    /// </summary>
    public override State RunCurrentState()
    {
        // reset the choose crisis states
        chooseCrisisState.CrisisChosen = false;
        chooseCrisisState.ChosenCrisis = null;
        chooseCrisisState.ChoosingCrisis = false;
        chooseCrisisEnemyState.ChosenCrisis = null;
        chooseCrisisEnemyState.CrisisChosen = false;
        chooseCrisisEnemyState.ChoosingCrisis = false;
        chooseCrisisNeutralState.ChosenCrisis = null;
        chooseCrisisNeutralState.CrisisChosen = false;
        chooseCrisisNeutralState.ChoosingCrisis = false;
        

        if (playerTurnComplete && stateManager.PlayerRelationship == PlayerRelationshipEnum.Ally)
        {
            playerTurnComplete = false;
            return chooseCrisisState;
        }
        if (playerTurnComplete && stateManager.PlayerRelationship == PlayerRelationshipEnum.Neutral)
        {
            playerTurnComplete = false;
            return chooseCrisisNeutralState;
        }
        if (playerTurnComplete && stateManager.PlayerRelationship == PlayerRelationshipEnum.Enemy)
        {
            playerTurnComplete = false;
            return chooseCrisisEnemyState;
        }
        return null;
    }

    /// <summary>
    /// sets the playerTurnComplete bool to true in response to the PlayerPlayedCard event
    /// </summary>
    public void SetPlayerTurnComplete()
    {
        PlayerTurnComplete = true;
    }



    void Start()
    {
        // subscribe to the player turn complete event on the crisis master
        crisisMaster = GameMaster.crisisMaster;
        stateManager = GameMaster.stateManager;
        crisisMaster.PlayerPlayedCardEvent.AddListener(SetPlayerTurnComplete);
    }

    
}
