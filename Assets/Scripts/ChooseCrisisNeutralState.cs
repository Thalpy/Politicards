using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChooseCrisisNeutralState : State
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
    [SerializeField] public ChooseCardNeutralState chooseCardState;

    #endregion


    #region Methods

    /// <summary>
    /// handles the flow of execution of the state
    /// once the AI has chosen a crisis to play, it will move into the ChooseCardNeutralState
    /// </summary>
    /// <returns>The next state</returns>
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
            if(crises[i] == null){continue;} // null check
            if (crises[i].crisis.factionProgress[aiFaction] >= highestProgress && crises[i].AICards[2] == null)
            {
                highestProgress = crises[i].crisis.factionProgress[aiFaction];
                selectedCrisis = crises[i];
            }
        }  
        CrisisChosen = true;
        return selectedCrisis;        
    }
    #endregion
}
