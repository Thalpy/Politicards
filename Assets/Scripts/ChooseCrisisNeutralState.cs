using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChooseCrisisNeutralState : State
{
    public bool CrisisChosen;

    bool choosingCrisis;

    ActiveCrisis chosenCrisis;

    public ActiveCrisis ChosenCrisis { get => chosenCrisis; set => chosenCrisis = value; }
    

    public bool ChoosingCrisis { get => choosingCrisis; set => choosingCrisis = value; }


    CrisisMaster crisisMaster = GameMaster.crisisMaster;

    StateManager stateManager = GameMaster.stateManager;

    [SerializeField] ChooseCardNeutralState chooseCardState;



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
            chosenCrisis = CrisisWithHighestProgressOfAiFaction(GameMaster.crisisMaster.ActiveCrisses);
            
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
    /// gets the crisis with the highest progress of the AI faction
    /// </summary>
    /// <param name="crises">The list of active crises</param>
    /// <returns>The active crisis with the highest progress of the AI faction</returns>
    public ActiveCrisis CrisisWithHighestProgressOfAiFaction(ActiveCrisis[] crises)
    {
        Faction aiFaction = GameMaster.stateManager.AiFaction;
        ActiveCrisis selectedCrisis = null;
        int highestProgress = 0;
        for (int i = 0; i < crises.Length; i++)
        {
            if (crises[i].crisis.factionProgress[aiFaction] > highestProgress)
            {
                highestProgress = crises[i].crisis.factionProgress[aiFaction];
                selectedCrisis = crises[i];
            }
        }  
        CrisisChosen = true;
        return selectedCrisis;        
    }
}
