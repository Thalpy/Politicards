using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    #region Events
    /// <summary>
    /// An event that takes a string as a parameter and informs subscribers that the player mana for a faction has changed
    /// </summary>
    [SerializeField] public ManaEvent playerManaChange = new ManaEvent();

    /// <summary>
    /// An event that takes a string as a parameter and informs subscribers that the ai mana for a faction has changed
    /// </summary>
    [SerializeField] public ManaEvent aiManaChange = new ManaEvent();

    /// <summary>
    /// An event that takes a PlayerHappinessEnum as a parameter and informs subscribers that the player happiness for a faction has changed
    /// </summary>
    [SerializeField] public PlayerHappinessEvent playerHappinessChange = new PlayerHappinessEvent();

    /// <summary>
    /// An event that takes a AIHappinessEnum as a parameter and informs subscribers that the ai happiness for a faction has changed
    /// </summary>
    [SerializeField] public AIHappinessEvent aiHappinessChange = new AIHappinessEvent();

    /// <summary>
    /// an event that informs subscribers that a factions power has changed
    /// </summary>
    [SerializeField] public UnityEvent factionPowerChange = new UnityEvent();

    [SerializeField] public List<Dialogue> endEvent = new List<Dialogue>();

    public bool playedEnding = false;


    #endregion

    /// <summary> 
    ///  Gets or sets the faction name
    /// </summary>
    /// <value>
    ///  The faction name as a string
    /// </value>
    [SerializeField] private string factionName;
    public string FactionName
    {
        get { return factionName; }
        set { factionName = value; }
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
        set
        {
            factionColor = value;
            //update the faction color in the game master

        }
    }



    /// <summary>
    /// an enum representing the factions happiness with the player
    /// Raises the playerHappinessChange event on setting the value
    /// </summary>
    [SerializeField] private PlayerHappinessEnum playerHappiness;
    public PlayerHappinessEnum PlayerHappiness
    {
        get { return playerHappiness; }
        set
        {
            playerHappiness = value;
            playerHappinessChange.Invoke(value);
        }
    }

    [SerializeField] private AIHappinessEnum aiHappiness;
    public AIHappinessEnum AIHappiness
    {
        get { return aiHappiness; }
        set
        {
            aiHappiness = value;
            aiHappinessChange.Invoke(value);
        }
    }

    /// <summary>
    ///  The faction power
    /// </summary>
    /// <value>
    ///  The faction power as a float
    ///  factionPowerChange event is fired on setting the value
    /// </value>
    [SerializeField] private float factionPower;
    public float FactionPower
    {
        get { return factionPower; }
        set
        {
            factionPower = value;
            factionPowerChange.Invoke();
        }
    }

    /// <summary>
    /// the player's mana as a float with getter and setter associated with this faction with a range of 0-100. 
    /// </summary>
    /// <value>
    /// The player's mana as a float
    /// </value>
    [SerializeField] private float playerMana;
    public float PlayerMana
    {
        get { return playerMana; }
        set
        {
            playerMana = value;
            // Invoke the player mana change event to let any subscribers know that the player's mana has changed
            playerManaChange.Invoke(value, factionName);
        }
    }

    /// <summary>
    /// the ais mana as a float with getter and setter associated with this faction with a range of 0-100.
    /// </summary>
    /// <value>
    /// The ai's mana as a float
    /// </value>
    [SerializeField] private float aiMana;
    public float AIMana
    {
        get { return aiMana; }
        set
        {
            aiMana = value;
            // Invoke the ai mana change event to let any subscribers know that the ai's mana has changed
            aiManaChange.Invoke(value, factionName);
        }
    }

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
        set 
        {
             factionHappiness = value;
             SetPlayerHappiness();
             playerHappinessChange.Invoke(playerHappiness);
        }
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
        set 
        {   
            factionAiHappiness = value;
            SetAiHappiness();
            aiHappinessChange.Invoke(aiHappiness);
        }
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
        if (factionHappiness > 10)
        {
            //if the faction happiness is already happy, do nothing
            if (playerHappiness == PlayerHappinessEnum.happy)
            {
                return;
            }
            PlayerHappiness = PlayerHappinessEnum.happy;
            happinessChanged = true;
        }
        else if (factionHappiness > 3)
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

    // a function that set the player happiness enum to a value appropriate for the faction happiness
    public void SetPlayerHappiness()
    {
        if (factionHappiness > 50)
        {
            PlayerHappiness = PlayerHappinessEnum.happy;
        }
        else if (factionHappiness > 25)
        {
            PlayerHappiness = PlayerHappinessEnum.neutral;
        }
        else
        {
            PlayerHappiness = PlayerHappinessEnum.unhappy;
        }
    }


    // a function that set the ai happiness enum to a value appropriate for the faction happiness
    public void SetAiHappiness()
    {
        if (factionHappiness > 50)
        {
            AIHappiness = AIHappinessEnum.happy;
        }
        else if (factionHappiness > 25)
        {
            AIHappiness = AIHappinessEnum.neutral;
        }
        else
        {
            AIHappiness = AIHappinessEnum.unhappy;
        }
    }

    ///<summary>
    /// awards mana to the player and the ai. mana = happiness * power
    /// </summary>
    public void AwardMana()
    {
        //if the player is happy, award the player mana
        if (playerHappiness == PlayerHappinessEnum.happy)
        {
            PlayerMana += factionPower;
        }
        //if the player is neutral, award the player mana
        else if (playerHappiness == PlayerHappinessEnum.neutral)
        {
            PlayerMana += factionPower / 2;
        }
        //if the player is unhappy, award the player mana
        else
        {
            PlayerMana += 0;
        }

        //if the ai is happy, award the ai mana
        if (aiHappiness == AIHappinessEnum.happy)
        {
            AIMana += factionPower;
        }
        //if the ai is neutral, award the ai mana
        else if (aiHappiness == AIHappinessEnum.neutral)
        {
            AIMana += factionPower / 2;
        }
        //if the ai is unhappy, award the ai mana
        else
        {
            AIMana += 0;
        }
    }




    /// <summary>
    ///  Change the faction AI happiness by a given amount
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

    public void EndGame()
    {
        if(playedEnding == false){
            GameMaster.dialoguePlayer.StartDialogue(endEvent);
            playedEnding = true;
        }
        Debug.Log("Ending for faction " + factionName + " has been played");
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
            if (FactionPower + amount < 0)
            {
                FactionPower = 0;
            }
            else
            {
                FactionPower = FactionPower + amount;
            }
        }
        //if the amount is positive, increase the power to a maximum of 100
        else
        {
            if (FactionPower + amount > 100)
            {
                FactionPower = 100;
            }
            else
            {
                FactionPower = FactionPower + amount;
            }
        }
    }
}
