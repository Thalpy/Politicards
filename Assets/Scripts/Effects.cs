using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to store the functionality of an effect.
/// </summary>
[System.Serializable]
public abstract class Effect
{
    //name 
    public string name = "ERROR"; //name setter
    //Strength of the effect
    public int power = 1;

    public virtual void setPower(int _power)
    {
        power = _power;
    }

    //Function that is called on the event box ending
    public virtual void DoEffect(){
        Debug.LogWarning("Attempted to call a result that doesn't exist!!");
    }

    //copies the object
    public virtual Effect Copy()
    {
        //creates a new object of the same type
        return System.Activator.CreateInstance(this.GetType()) as Effect;
    }
}

/// <summary>
/// Draws a card from the deck.
/// </summary>
public class DrawCard : Effect
{
    public DrawCard()
    {
        name = "Draw";
    }
    //Text that appears in the description box
    public override void DoEffect()
    {
        //repeat the draw card effect for the power
        for (int i = 0; i < power; i++)
        {
            //draw a card
            GameMaster.handController.DrawCard();
        }
    }
}

/// <summary>
/// Discards a card from the hand.
/// </summary>
public class DiscardCard : Effect
{
    public DiscardCard()
    {
        name = "Discard";
    }
    //Text that appears in the description box
    public override void DoEffect()
    {
        //repeat the draw card effect for the power
        for (int i = 0; i < power; i++)
        {
            Debug.Log("Discarding card");
            Debug.LogWarning("LANDY SEMPAI PLEASE FULFILL ME!!!!");
        }
    }

// TODO: Add a use effects on death proc ( deathrattle )
}
