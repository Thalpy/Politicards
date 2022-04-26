using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwaitPlayerState : State
{
    public bool playerTurnComplete;

    CrisisMaster crisisMaster = GameMaster.crisisMaster;

    [SerializeField] ChooseCrisisAllyState chooseCrisisState;

    [SerializeField] ChooseCrisisEnemyState chooseCrisisEnemyState;

    [SerializeField] ChooseCrisisNeutralState chooseCrisisNeutralState;

    public override State RunCurrentState()
    {
        if (playerTurnComplete)
        {
            playerTurnComplete = false;
            return chooseCrisisState;
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
