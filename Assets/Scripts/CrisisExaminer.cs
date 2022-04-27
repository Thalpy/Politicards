using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrisisExaminer
{

    [SerializeField] ActiveCrisis[] crises;
    public ActiveCrisis[] Crises
    {
        get { return crises;}

        set { crises = value; }
    }

    //constructor
    public CrisisExaminer(ActiveCrisis[] crises)
    {
        this.crises = crises;    
    }

    /// <summary>
    /// returns the crisis which is most likley to breach the minimum progress threshold
    /// the ai should choose the result of this function if they have a good relationship with the player.
    /// </summary>
    /// <returns>the crisis most likley to complete</returns>
    public ActiveCrisis CrisisMostLikleyToComplete()
    {
        int mostLikelyCrisisValue = 0;
        int mostLikleyCrisisIndex = 0;
        for(int i = 0; i < crises.Length; i++)
        {
            Crisis crisis = Crises[i].crisis;
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
        return crises[mostLikleyCrisisIndex];
    }

    /// <summary>
    /// returns the crisis where the ai's faction has the highest faction progress
    /// the ai should choose the result of this function if they have a neutral relatonship with the player.
    /// </summary>
    /// <returns>the crisis with the highest faction progress</returns>
    public ActiveCrisis CrisisWithHighestProgressOfAiFaction()
    {
        Faction aiFaction = GameMaster.stateManager.AiFaction;
        int highestProgress = 0;
        int highestProgressIndex = 0;
        for(int i = 0; i < crises.Length; i++)
        {
            Crisis crisis = Crises[i].crisis;
            int currentProgress = crisis.factionProgress[aiFaction];
            if(currentProgress > highestProgress)
            {
                highestProgress = currentProgress;
                highestProgressIndex = i;
            }
        }
        return crises[highestProgressIndex];
    }

    /// <summary>
    /// returns the crisis where the player's faction has the lowest faction progress
    /// the ai should choose the result of this function if they have a bad relationship with the player.
    /// </summary>
    /// <returns>the crisis with the lowest faction progress</returns>
    public ActiveCrisis CrisisWithLowestProgressOfPlayerFaction()
    {
        Faction playerFaction = GameMaster.stateManager.PlayerFaction;
        int lowestProgress = int.MaxValue;
        int lowestProgressIndex = 0;
        for(int i = 0; i < crises.Length; i++)
        {
            Crisis crisis = Crises[i].crisis;
            int currentProgress = crisis.factionProgress[playerFaction];
            if(currentProgress < lowestProgress)
            {
                lowestProgress = currentProgress;
                lowestProgressIndex = i;
            }
        }
        return crises[lowestProgressIndex];
    }



}
