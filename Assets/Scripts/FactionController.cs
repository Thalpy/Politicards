using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to hold references to all the factions 
/// provides methods to access and modify the factions
/// </summary>
public class FactionController : MonoBehaviour
{

    /// <summary>
    /// The list of factions
    /// </summary>
    /// <typeparam name="Faction">Individual faction objects</typeparam>
    /// <returns></returns>
     [SerializeField] List<Faction> factions = new List<Faction>();

    /// <summary>
    /// Reference to the game master
    /// </summary>
     GameMaster gameMaster;

    void Start()
    {
        //get the game master
        gameMaster = GetComponent<GameMaster>();
        
    }

    /// <summary>
    /// Gets the power of all factions
    /// </summary>
    /// <returns>a float array of the faction powers</returns>
    public float[] GetFactionPower()
    {
        float[] factionPower = new float[factions.Count];
        for (int i = 0; i < factions.Count; i++)
        {
            factionPower[i] = factions[i].FactionPower;
        }
        return factionPower;
    }

    /// <summary>
    /// Gets the happiness of all factions
    /// </summary>
    /// <returns>
    /// a float array of the faction happiness
    ///</returns>
    public float[] GetFactionHappiness()
    {
        float[] factionHappiness = new float[factions.Count];
        for (int i = 0; i < factions.Count; i++)
        {
            factionHappiness[i] = factions[i].FactionHappiness;
        }
        return factionHappiness;
    }

    /// <summary>
    /// Gets the happiness of all factions with the AI
    /// </summary>
    /// <returns>
    /// a float array of the faction happiness with the AI
    /// </returns>
    public float[] GetFactionAiHappiness()
    {
        float[] factionAiHappiness = new float[factions.Count];
        for (int i = 0; i < factions.Count; i++)
        {
            factionAiHappiness[i] = factions[i].FactionAiHappiness;
        }
        return factionAiHappiness;
    }

    /// <summary>
    /// Given a faction name, returns the faction object
    /// </summary>
    /// <param name="factionName">
    /// The name of the faction to get
    /// </param>
    /// <returns>
    ///    The faction object or null if the faction name is not found
    /// </returns>
    public Faction SelectFaction(string factionName)
    {
        for (int i = 0; i < factions.Count; i++)
        {
            if (factions[i].FactionName == factionName)
            {
                return factions[i];
            }
        }
        Debug.LogWarning("Faction not found");
        Debug.Break();
        return null;
    }

    public Faction SelectFaction(int factionIndex)
    {
        return factions[factionIndex];
    }

    /// <summary>
    ///  Given a faction name, returns the faction power
    /// </summary>
    /// <param name="factionName">
    ///  The name of the faction to get the power of
    /// </param>
    /// <returns>
    /// The faction power or 0 if the faction name is not found
    /// </returns>
    public float GetFactionPower(string factionName)
    {
        for (int i = 0; i < factions.Count; i++)
        {
            if (factions[i].FactionName == factionName)
            {
                return factions[i].FactionPower;
            }
        }
        Debug.LogWarning("Faction not found");
        Debug.Break();
        return 0;
    }

    /// <summary>
    /// Given a faction name and a power amount, changes the faction power
    /// </summary>
    /// <param name="factionName">
    ///  The name of the faction to change the power of
    /// </param>
    /// <param name="amount">
    ///  The amount to change the faction power by
    /// </param>
    public void ChangeFactionPower(string factionName, int amount)
    {
        for (int i = 0; i < factions.Count; i++)
        {
            if (factions[i].FactionName == factionName)
            {
                factions[i].ChangePower(amount);
            }
        }
    }


    /// <summary>
    /// Given a faction name, returns the faction happiness
    /// </summary>
    /// <param name="factionName">
    ///  The name of the faction to get the happiness of
    /// </param>
    /// <returns>
    ///  The faction happiness or 0 if the faction name is not found
    /// </returns>
    public float GetFactionHappiness(string factionName)
    {
        for (int i = 0; i < factions.Count; i++)
        {
            if (factions[i].FactionName == factionName)
            {
                return factions[i].FactionHappiness;
            }
        }
        Debug.LogWarning("Faction not found");
        Debug.Break();
        return 0;
    }

    /// <summary>
    ///  Given a faction name, get the faction happiness with the AI
    /// </summary>
    /// <param name="factionName">
    ///  The name of the faction to get the happiness with the AI of
    /// </param>
    /// <returns>
    ///     The faction happiness with the AI or 0 if the faction name is not found
    /// </returns>
    public float GetFactionAiHappiness(string factionName)
    {
        for (int i = 0; i < factions.Count; i++)
        {
            if (factions[i].FactionName == factionName)
            {
                return factions[i].FactionAiHappiness;
            }
        }
        Debug.LogWarning("Faction not found");
        Debug.Break();
        return 0;
    }


   /// <summary>
   /// Given a faction name and a happiness amount, changes the faction happiness
   /// </summary>
   /// <param name="factionName">
   ///  The name of the faction to change the happiness of
   /// </param>
   /// <param name="amount">
   ///  The amount to change the faction happiness by
   /// </param>
    public void ChangeFactionHappiness(string factionName, float amount)
    {
        for (int i = 0; i < factions.Count; i++)
        {
            if (factions[i].FactionName == factionName)
            {
                factions[i].ChangeHappiness(amount);
            }
        }
    }

    /// <summary>
    /// Given a faction name and a happiness amount, changes the faction happiness with the AI
    /// </summary>
    /// <param name="factionName">
    ///  The name of the faction to change the happiness with the AI of
    /// </param>
    /// <param name="amount">
    ///  The amount to change the faction happiness with the AI by
    /// </param>
    public void ChangeFactionAiHappiness(string factionName, float amount)
    {
        for (int i = 0; i < factions.Count; i++)
        {
            if (factions[i].FactionName == factionName)
            {
                factions[i].ChangeAiHappiness(amount);
            }
        }
    }



    /// <summary>
    ///  Get a list of all the faction names
    /// </summary>
    /// <returns>
    ///  An array of all the faction names
    /// </returns>
    public string[] GetFactionNames()
    {
        string[] factionNames = new string[factions.Count];
        for (int i = 0; i < factions.Count; i++)
        {
            factionNames[i] = factions[i].FactionName;
        }
        return factionNames;
    }

    /// <summary>
    ///  Get a list of all the faction names with the AI
    /// </summary>
    /// <returns>
    ///  An int array of all the faction names with the AI
    /// </returns>
    public int GetNumberOfFactions()
    {
        return factions.Count;
    }

}
