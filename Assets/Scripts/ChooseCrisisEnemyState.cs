using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The state the AI will be in when it is choosing a crisis and has a bad relationship with the player
/// </summary>
public class ChooseCrisisEnemyState : State
{
    #region Fields
    /// <summary>
    /// true: the AI has chosen a crisis
    /// false: the AI has not chosen a crisis
    /// </summary>
    public bool CrisisChosen;

    /// <summary>
    /// true: the AI is choosing a crisis
    /// false: the AI has chosen a crisis
    /// </summary>
    bool choosingCrisis;
    public bool ChoosingCrisis { get => choosingCrisis; set => choosingCrisis = value; }
    public ActiveCrisis ChosenCrisis { get => chosenCrisis; set => chosenCrisis = value; }

    Faction playerFaction;
    /// <summary>
    /// the crisis the AI has chosen
    /// </summary>
    public ActiveCrisis chosenCrisis;      
    CrisisMaster crisisMaster = GameMaster.crisisMaster;

    StateManager stateManager = GameMaster.stateManager;
    
    /// <summary>
    /// the state the AI will move into once it has chosen a crisis
    /// </summary>
    [SerializeField] ChooseCardEnemyState chooseCardState;

    #endregion

    void Start()
    {
        playerFaction = GameMaster.stateManager.PlayerFaction;        
    }

    /// <summary>
    /// handles the flow of execution of the state
    /// once the AI has chosen a crisis to play, it will move into the ChooseCardEnemyState
    /// </summary>
    public override State RunCurrentState()
    {
        // log the value of CrisisChosen and ChoosingCrisis to the console
        Debug.Log("CrisisChosen: " + CrisisChosen + " ChoosingCrisis: " + ChoosingCrisis);
        if (CrisisChosen && chosenCrisis != null)
        {
            CrisisChosen = false; 
            chooseCardState.ActiveCrisis = chosenCrisis;         
            return chooseCardState;
        }
        if (!choosingCrisis && chosenCrisis == null)
        {
            choosingCrisis = true;
            chosenCrisis = CrisisWIthLowestProgressOfPlayerFaction(GameMaster.crisisMaster.ActiveCrisses);
            CrisisChosen = true;
            choosingCrisis = false;
            
            
        }
        return null;
    }


    /// <summary>
    /// set the Crisis Chosen bool once the AI has chosen a crisis
    /// </summary>
    public void SetCrisisChosen()
    {
        CrisisChosen = true;
    }

    /// <summary>
    /// Gets the list of crises in play
    /// </summary>    
    public ActiveCrisis[] GetCrisisList()
    {
        return crisisMaster.ActiveCrisses;
    }

    /// <summary>
    /// gets the crisis with the lowest progress of the player's faction
    /// </summary>
    /// <param name="crises">list of all crises</param>
    /// <returns>the crisis with the lowest progress of the player's faction</returns>
    public ActiveCrisis CrisisWIthLowestProgressOfPlayerFaction(ActiveCrisis[] crises)
    {
         ActiveCrisis lowestProgressCrisis = null;
         int lowestProgress = int.MaxValue;
         for (int i = 0; i < crises.Length; i++)
        {
            ActiveCrisis currentCrisis = crises[i];
            
            if (currentCrisis == null) { continue; }
            int FactionProgress = FactionProgressOnCard(crises, i);
            if (FactionProgress <= lowestProgress && crises[i].AICards[2] == null)
            {
                lowestProgress = FactionProgress;
                lowestProgressCrisis = crises[i];
            }

        }
        //throw an error if we try to return null
        if (lowestProgressCrisis == null)
        {
            Debug.LogError("No crisis with lowest progress");
            Debug.Break();
        }
        return lowestProgressCrisis;

        int FactionProgressOnCard(ActiveCrisis[] crises, int i)
        {
            Dictionary<Faction, int> factionProgress = crises[i].crisis.factionProgress;
            int FactionProgressOnCard = factionProgress[playerFaction];
            return FactionProgressOnCard;
        }
    }
}
