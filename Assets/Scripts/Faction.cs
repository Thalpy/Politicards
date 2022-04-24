using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Faction
{
    //player happiness enum
    public enum PlayerHappinessEnum
    {
        happy,
        neutral,
        unhappy
    }

    //ai happiness enum
    public enum AIHappinessEnum
    {
        happy,
        neutral,
        unhappy
    }

    /// <summary> 
    ///  Gets or sets the faction name
    /// </summary>
    /// <value>
    ///  The faction name as a string
    /// </value>
    [SerializeField] private string factionName; //make this field adjustable in the inspector
    public string FactionName
    {
        get { return factionName; }
        set { factionName = value;
            //update the faction name in the game master
            //Happiness level has changed- call function to process effect here
        }
    }

    /// <summary> 
    ///  Gets or sets the faction color
    /// </summary>
    /// <value>
    ///  The faction color as a color
    /// </value>
    [SerializeField] private Color factionColor; //make this field adjustable in the inspector
    public Color FactionColor
    {
        get { return factionColor; }
        set { factionColor = value;
            //update the faction color in the game master

        }
    }



    // enum fields with getters and setters
    [SerializeField] private PlayerHappinessEnum playerHappiness;
    public PlayerHappinessEnum PlayerHappiness
    {
        get { return playerHappiness; }
        set { playerHappiness = value;
            //happiness level has changed- call function to process effect here
         }
    }

    [SerializeField] private AIHappinessEnum aiHappiness;
    public AIHappinessEnum AIHappiness
    {
        get { return aiHappiness; }
        set { aiHappiness = value; }
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

    //The icon of the faction
    [SerializeField] private Sprite factionIcon;    





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

        bool happinessChanged = false;
        //if the faction happiness is greater than 50, set the faction happiness to happy, if it is between 25 and 50, set it to neutral, and if it is less than 25, set it to unhappy
        if (factionHappiness > 50)
        {
            //if the faction happiness is already happy, do nothing
            if (playerHappiness == PlayerHappinessEnum.happy)
            {
                return;
            }
            PlayerHappiness = PlayerHappinessEnum.happy;
            happinessChanged = true;
        }
        else if (factionHappiness > 25)
        {
            //if the faction happiness is already neutral, do nothing
            if (playerHappiness == PlayerHappinessEnum.neutral)
            {
                return;
            }
            PlayerHappiness = PlayerHappinessEnum.neutral;
            happinessChanged = true;
        }
        else
        {
            //if the faction happiness is already unhappy, do nothing
            if (playerHappiness == PlayerHappinessEnum.unhappy)
            {
                return;
            }
            PlayerHappiness = PlayerHappinessEnum.unhappy;
            happinessChanged = true;
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

        //if the faction happiness with the AI is greater than 50, set the faction happiness with the AI to happy, if it is between 25 and 50, set it to neutral, and if it is less than 25, set it to unhappy
        if (factionAiHappiness > 50)
        {
            //if the faction happiness with the AI is already happy, do nothing
            if (aiHappiness == AIHappinessEnum.happy)
            {
                return;
            }
            AIHappiness = AIHappinessEnum.happy;
        }
        else if (factionAiHappiness > 25)
        {
            //if the faction happiness with the AI is already neutral, do nothing
            if (aiHappiness == AIHappinessEnum.neutral)
            {
                return;
            }
            AIHappiness = AIHappinessEnum.neutral;
        }
        else
        {
            //if the faction happiness with the AI is already unhappy, do nothing
            if (aiHappiness == AIHappinessEnum.unhappy)
            {
                return;
            }
            AIHappiness = AIHappinessEnum.unhappy;
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
