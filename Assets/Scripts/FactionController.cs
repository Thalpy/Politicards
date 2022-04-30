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

    [SerializeField] public ManaEvent ManaEvent = new ManaEvent();

    /// <summary>
    /// a dictionary of all the factions vs the legacy index of the faction
    /// </summary>
    public Dictionary<string, int> FactionDictionary {get; set;}

    public void onPlayerManaChange(float mana, string factionName)
    {
        Debug.Log("Mana change event received");
       this.ManaEvent.Invoke(mana, factionName);
       // say firing off the event with the faction name and mana amount
       Debug.Log("FactionController.onPlayerManaChange: " + factionName + " " + mana);
    }


    void Start()
    {
        
        //get the game master
        gameMaster = GetComponent<GameMaster>();
        FactionDictionary = new Dictionary<string, int>();
        int i = 0;
        foreach(Faction faction in factions)
        {
            //subscribe to the faction's mana change event
            faction.playerManaChange.AddListener(onPlayerManaChange);
            FactionDictionary.Add(faction.FactionName, i);
            faction.ChangeHappiness(5);
            faction.ChangeAiHappiness(5);
            i++;
        }

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

    public Faction GetRandomFaction(bool crime = false)
    {
        if(crime)
        {
            return factions[UnityEngine.Random.Range(0, factions.Count)];
        }
        else
        {
            return factions[UnityEngine.Random.Range(0, factions.Count-1)];
        }
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
        Debug.LogWarning("Faction not found:" + factionName);
        return null;
    }

    public Faction CheckIfFactionsOver50()
    {
        float sumPower = 0;
        for (int i = 0; i < factions.Count; i++)
        {
            sumPower += factions[i].FactionPower;
        }
        for (int i = 0; i < factions.Count; i++)
        {
            if (factions[i].FactionPower > sumPower / 2)
            {
                return factions[i];
            }
        }
        return null;
    }

    public Faction SelectFaction(int factionIndex)
    {
        if(factionIndex > factions.Count - 1)
        {
            Debug.LogWarning("Faction not found:" + factionIndex);
        }
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
    ///  Given a faction name, get the faction happiness with the AI
    /// </summary>
    /// <param name="factionName">
    ///  The name of the faction to get the happiness with the AI of
    /// </param>
    /// <returns>
    ///     The faction color or white if the faction name is not found
    /// </returns>
    public Color GetFactionColor(string factionName)
    {
        for (int i = 0; i < factions.Count; i++)
        {
            if (factions[i].FactionName == factionName)
            {
                return factions[i].FactionColor;
            }
        }
        Debug.LogWarning("Faction not found");
        Debug.Break();
        return Color.white;
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

    // a function to award faction specific mana to players based upon faction happiness and power
    public void AwardMana()
    {
        for (int i = 0; i < factions.Count; i++)
        {
            factions[i].AwardMana();
        }
    }

    //a function to get the faction specific mana, if "player" is passed in then it will return the faction specific mana for the player otherwise it will return the faction specific mana for the AI
    public float GetMana(string factionName, string player)
    {
        for (int i = 0; i < factions.Count; i++)
        {
            if (factions[i].FactionName == factionName)
            {
                if (player == "player")
                {
                    return factions[i].PlayerMana;
                }
                else
                {
                    return factions[i].AIMana;
                }
            }
        }
        Debug.LogWarning("Faction not found");
        Debug.Break();
        return 0;
    }

    //a function to get the faction specific mana for the ai
    public float GetAiMana(string factionName)
    {
        for (int i = 0; i < factions.Count; i++)
        {
            if (factions[i].FactionName == factionName)
            {
                return factions[i].AIMana;
            }
        }
        Debug.LogWarning("Faction not found");
        Debug.Break();
        return 0;
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

    public List<Faction> GetFactions()
    {
        return factions;
    }

    //test function to send a random mana value between 0 and 1 to a random factions mana bar
    public void SendRandomMana()
    {
        int randomFaction = UnityEngine.Random.Range(0, factions.Count);
        // a random float between 0 and 1
        float randomMana = UnityEngine.Random.Range(0f, 1f);
        Debug.Log("Sending mana: " + randomMana + " to faction: " + factions[randomFaction].FactionName);
        factions[randomFaction].PlayerMana = randomMana;
    }

    public void ChangeRandomPower()
    {
        int randomFaction = UnityEngine.Random.Range(0, factions.Count);
        // a random float between 0 and 1
        int randomPower = UnityEngine.Random.Range(-100, 100);
        Debug.Log("Changing power: " + randomPower + " to faction: " + factions[randomFaction].FactionName);
        factions[randomFaction].ChangePower(randomPower);
    }

}
