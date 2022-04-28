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
    StateManager stateManager = GameMaster.stateManager;

    CrisisMaster crisisMaster = GameMaster.crisisMaster;

    /// <summary>
    /// the state the AI will move into once the PlayerPlayedCard event has been recieved if the player has a good relationship with the AI
    /// </summary>
    [SerializeField] ChooseCrisisAllyState chooseCrisisState;

    /// <summary>
    /// the state the AI will move into once the PlayerPlayedCard event has been recieved if the player has a bad relationship with the AI
    /// </summary>
    [SerializeField] ChooseCrisisEnemyState chooseCrisisEnemyState;

    /// <summary>
    /// the state the AI will move into once the PlayerPlayedCard event has been recieved if the player has a neutral relationship with the AI
    /// </summary>
    [SerializeField] ChooseCrisisNeutralState chooseCrisisNeutralState;

    /// <summary>
    /// handles the logic for determining which crisis state the AI will move into
    /// makes sure that the choose crisis states do not have a chosen crisis before moving into them
    /// </summary>
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

    /// <summary>
    /// sets the playerTurnComplete bool to true in response to the PlayerPlayedCard event
    /// </summary>
    public void SetPlayerTurnComplete()
    {
        PlayerTurnComplete = true;
    }


    void Awake()
    {
        // subscribe to the player turn complete event on the crisis master
        crisisMaster.PlayerPlayedCardEvent.AddListener(SetPlayerTurnComplete);
    }

    
}
