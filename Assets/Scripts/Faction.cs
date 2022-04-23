using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Faction
{
    /// <summary>
    /// name of the faction
    /// </summary>
    [SerializeField] private string factionName; //make this field adjustable in the inspector

    /// <summary> 
    ///  Gets or sets the faction name
    /// </summary>
    /// <value>
    ///  The faction name as a string
    /// </value>
    public string FactionName
    {
        get { return factionName; }
        set { factionName = value; }
    }

    /// <summary>
    ///  The faction power
    /// </summary>
    /// <value>
    ///  The faction power as a float
    /// </value>
    [SerializeField] private float factionPower;
    public float FactionPower
    {
        get { return factionPower; }
        set { factionPower = value; }
    }
    //

    /// <summary>
    ///   The faction happiness
    /// </summary>
    /// <value>
    ///  The faction happiness as a float
    /// </value>
    [SerializeField] private float factionHappiness;
    public float FactionHappiness
    {
        get { return factionHappiness; }
        set { factionHappiness = value; }
    }

    /// <summary>
    ///  The faction happiness with the AI
    /// </summary>
    /// <value>
    ///  The faction happiness with the AI as a float
    /// </value>
    [SerializeField] private float factionAiHappiness;
    public float FactionAiHappiness
    {
        get { return factionAiHappiness; }
        set { factionAiHappiness = value; }
    }

    /// <summary>
    ///  changes the faction happiness by a given amount
    /// </summary>
    /// <param name="amount">
    ///  The amount to change the faction happiness by
    /// </param>
    public void ChangeHappiness(float amount)
    {
        // if the amount is 0, do nothing
        if (amount == 0)
        {
            return;
        }
        //if the amount is positive, add the amount to the faction happiness and clamp it to a max of 100
        if (amount > 0)
        {
            factionHappiness += amount;
            if (factionHappiness > 100)
            {
                factionHappiness = 100;
            }
        }
        //if the amount is negative, subtract the amount from the faction happiness and clamp it to a min of 0
        else
        {
            factionHappiness += amount;
            if (factionHappiness < 0)
            {
                factionHappiness = 0;
            }
        }
    }

    /// <summary>
    ///  
    /// </summary>
    /// <param name="amount">
    ///  The amount to change the faction happiness with the AI by
    /// </param>
    public void ChangeAiHappiness(float amount)
    {
        // if the amount is 0, do nothing
        if (amount == 0)
        {
            return;
        }
        //if the amount is positive, add the amount to the faction happiness with the AI and clamp it to a max of 100
        if (amount > 0)
        {
            factionAiHappiness += amount;
            if (factionAiHappiness > 100)
            {
                factionAiHappiness = 100;
            }
        }
        //if the amount is negative, subtract the amount from the faction happiness with the AI and clamp it to a min of 0
        else
        {
            factionAiHappiness += amount;
            if (factionAiHappiness < 0)
            {
                factionAiHappiness = 0;
            }
        }
    }

    /// <summary>
    /// Changes the faction power by a given amount
    /// </summary>
    /// <param name="amount">
    ///  The amount to change the faction power by
    /// </param>
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
