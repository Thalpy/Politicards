using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionController : MonoBehaviour
{
    // Start is called before the first frame update

    // a serializable public array of all the factions

     [SerializeField] List<Faction> factions = new List<Faction>();

     GameMaster gameMaster;

    void Start()
    {
        gameMaster = GetComponent<GameMaster>();
        
    }

    // a function to get the power of each faction in factions
    public float[] GetFactionPower()
    {
        float[] factionPower = new float[factions.Count];
        for (int i = 0; i < factions.Count; i++)
        {
            factionPower[i] = factions[i].FactionPower;
        }
        return factionPower;
    }

    // a function to get the happiness of each faction in factions
    public float[] GetFactionHappiness()
    {
        float[] factionHappiness = new float[factions.Count];
        for (int i = 0; i < factions.Count; i++)
        {
            factionHappiness[i] = factions[i].FactionHappiness;
        }
        return factionHappiness;
    }

    // a function to get the happiness with ai of each faction in factions
    public float[] GetFactionAiHappiness()
    {
        float[] factionAiHappiness = new float[factions.Count];
        for (int i = 0; i < factions.Count; i++)
        {
            factionAiHappiness[i] = factions[i].FactionAiHappiness;
        }
        return factionAiHappiness;
    }

    // a function to select a faction by name
    public Faction SelectFaction(string factionName)
    {
        for (int i = 0; i < factions.Count; i++)
        {
            if (factions[i].FactionName == factionName)
            {
                return factions[i];
            }
        }
        return null;
    }

    //a function to get the power of a faction by name
    public float GetFactionPower(string factionName)
    {
        for (int i = 0; i < factions.Count; i++)
        {
            if (factions[i].FactionName == factionName)
            {
                return factions[i].FactionPower;
            }
        }
        return 0;
    }

    //a function to change the power of a faction by name
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


    // a function to get the happiness of a faction by name
    public float GetFactionHappiness(string factionName)
    {
        for (int i = 0; i < factions.Count; i++)
        {
            if (factions[i].FactionName == factionName)
            {
                return factions[i].FactionHappiness;
            }
        }
        return 0;
    }

    // a function to get the happiness with ai of a faction by name
    public float GetFactionAiHappiness(string factionName)
    {
        for (int i = 0; i < factions.Count; i++)
        {
            if (factions[i].FactionName == factionName)
            {
                return factions[i].FactionAiHappiness;
            }
        }
        return 0;
    }


    // a function to change the happiness of a faction by its name
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

    // a function to change the happiness with ai of a faction by its name
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



    // get the names of all the factions
    public string[] GetFactionNames()
    {
        string[] factionNames = new string[factions.Count];
        for (int i = 0; i < factions.Count; i++)
        {
            factionNames[i] = factions[i].FactionName;
        }
        return factionNames;
    }



}
