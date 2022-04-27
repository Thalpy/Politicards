using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChooseCrisisEnemyState : State
{
    public bool CrisisChosen;

    bool choosingCrisis;

    Faction playerFaction = GameMaster.stateManager.PlayerFaction;

    public ActiveCrisis chosenCrisis;

    public ActiveCrisis ChosenCrisis { get => chosenCrisis; set => chosenCrisis = value; }
    

    public bool ChoosingCrisis { get => choosingCrisis; set => choosingCrisis = value; }


    CrisisMaster crisisMaster = GameMaster.crisisMaster;

    StateManager stateManager = GameMaster.stateManager;

    [SerializeField] ChooseCardAllyState chooseCardState;



    public override State RunCurrentState()
    {
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

    // a function that gets a list of all crises currrntly in play
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
         int lowestProgress = 0;
         for (int i = 0; i < crises.Length; i++)
         {
             if(crises[i].crisis.factionProgress[playerFaction] < lowestProgress)
             {
                 lowestProgress = crises[i].crisis.factionProgress[playerFaction];
                 lowestProgressCrisis = crises[i];
             }

         }
        return lowestProgressCrisis;
    }
}
