using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Faction
{
    //private string factionName with getter and setter
    [SerializeField] private string factionName; //make this field adjustable in the inspector
    public string FactionName
    {
        get { return factionName; }
        set { factionName = value; }
    }

    //private int factionPower with getter and setter
    [SerializeField] private float factionPower;
    public float FactionPower
    {
        get { return factionPower; }
        set { factionPower = value; }
    }
    //

    //a private float that represents the faction's current happiness with the player
    [SerializeField] private float factionHappiness;
    public float FactionHappiness
    {
        get { return factionHappiness; }
        set { factionHappiness = value; }
    }

    //a private float that represents the faction's current happiness with the AI  
    [SerializeField] private float factionAiHappiness;
    public float FactionAiHappiness
    {
        get { return factionAiHappiness; }
        set { factionAiHappiness = value; }
    }

    //a function to change the faction's happiness
    public void ChangeHappiness(float amount)
    {
        factionHappiness += amount;
    }

    //a function to change the faction's happiness with the ai
    public void ChangeAiHappiness(float amount)
    {
        factionAiHappiness += amount;
    }

    //a function to change the faction's power
    public void ChangePower(int amount)
    {
        //if the amount is negative, decrease the power to a minimum of zero
        if (amount < 0)
        {
            if (factionPower + amount < 0)
            {
                factionPower = 0;
            }
            else
            {
                factionPower += amount;
            }
        }
        //if the amount is positive, increase the power to a maximum of 100
        else
        {
            if (factionPower + amount > 100)
            {
                factionPower = 100;
            }
            else
            {
                factionPower += amount;
            }
        }
    }
}
