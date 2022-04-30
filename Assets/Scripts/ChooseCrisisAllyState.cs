using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The state the AI will be in when it is choosing a crisis and has a good relationship with the player
/// </summary>
public class ChooseCrisisAllyState : State
{
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

    /// <summary>
    /// the crisis the AI has chosen
    /// </summary>
    ActiveCrisis chosenCrisis;

    public ActiveCrisis ChosenCrisis { get => chosenCrisis; set => chosenCrisis = value; }
    CrisisMaster crisisMaster = GameMaster.crisisMaster;

    StateManager stateManager = GameMaster.stateManager;

    /// <summary>
    /// the state the AI will move into once it has chosen a crisis
    /// </summary>
    [SerializeField] public ChooseCardAllyState chooseCardState;


    /// <summary>
    /// handles the flow of execution of the state
    /// once the AI has chosen a crisis to play, it will move into the ChooseCardAllyState
    /// </summary>
    public override State RunCurrentState()
    {
        chooseCardState.ActiveCrisis = null;
        chooseCardState.cardChosen = false;
        chooseCardState.choosingCard = false;
        chooseCardState.chosenCard = null;
        
        if (CrisisChosen && chosenCrisis != null)
        {
            CrisisChosen = false;
            chooseCardState.ActiveCrisis = chosenCrisis;       
            return chooseCardState;
        }
        if (!choosingCrisis && chosenCrisis == null)
        {
            choosingCrisis = true;
            chosenCrisis = CrisisMostLikleyToComplete(GameMaster.crisisMaster.ActiveCrisses);
            choosingCrisis = false;
            CrisisChosen = true;
            
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
    /// a function that gets a list of all crises currrntly in play
    /// </summary>
    /// <returns>The list of currently active crises</returns>
    public ActiveCrisis[] GetCrisisList()
    {
        return crisisMaster.ActiveCrisses;
    }

    /// <summary>
    /// Gets the crisis which is closest to a successful outcome
    /// </summary>
    /// <param name="crises">The list of active crises</param>
    /// <returns>The active crisis which is closest to a successful outcome - The AI will play on this crisis.</returns>
    public ActiveCrisis CrisisMostLikleyToComplete(ActiveCrisis[] crises)
    {
        int mostLikelyCrisisValue = 0;
        int mostLikleyCrisisIndex = 0;
        for(int i = 0; i < crises.Length; i++)
        {
            if (crises[i] == null){continue;}
            ActiveCrisis currentActiveCrisis = crises[i];
            Crisis crisis = crises[i].crisis; //TODO add a null check
            int[] currentProgress = crisis.GetProgress();
            // get the max value of current progress
            int maxValue = currentProgress.Max();
            int currentProgDiff = crisis.minProgress - maxValue;
            if(currentProgDiff > mostLikelyCrisisValue && currentActiveCrisis.AICards[2] == null)
            {
                mostLikelyCrisisValue = currentProgDiff;
                mostLikleyCrisisIndex = i;
            }
            {
                mostLikelyCrisisValue = currentProgDiff;
                mostLikleyCrisisIndex = i;
            }
        }
        choosingCrisis = false;
        return crises[mostLikleyCrisisIndex];
    }
}
