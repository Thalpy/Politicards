using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChooseCrisisEnemyState : State
{
    public bool CrisisChosen;

    bool choosingCrisis;

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
            return chooseCardState;
        }
        if (!choosingCrisis && chosenCrisis == null)
        {
            choosingCrisis = true;
            chosenCrisis = CrisisMostLikleyToComplete(GameMaster.crisisMaster.ActiveCrisses);
            
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

    public ActiveCrisis CrisisMostLikleyToComplete(ActiveCrisis[] crises)
    {
        int mostLikelyCrisisValue = 0;
        int mostLikleyCrisisIndex = 0;
        for(int i = 0; i < crises.Length; i++)
        {
            Crisis crisis = crises[i].crisis;
            int[] currentProgress = crisis.GetProgress();
            // get the max value of current progress
            int maxValue = currentProgress.Max();
            int currentProgDiff = crisis.minProgress - maxValue;
            if(currentProgDiff > mostLikelyCrisisValue)
            {
                mostLikelyCrisisValue = currentProgDiff;
                mostLikleyCrisisIndex = i;
            }
        }
        choosingCrisis = false;
        return crises[mostLikleyCrisisIndex];
    }
}
