using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to store the functionality of an effect.
/// </summary>
public abstract class Effect
{
    //name 
    public string Name = "ERROR"; //name setter

    //Function that is called on the event box ending
    public virtual void DoEffect(){
        Debug.LogWarning("Attempted to call a result that doesn't exist!!");
    }
}

/// <summary>
/// Draws a card from the deck.
/// </summary>
public class DrawCard : Effect
{
    public new string Name = "Draw card";
    //Text that appears in the description box
    public override void DoEffect()
    {
        GameMaster.handController.DrawCard();
    }
}

/// <summary>
/// Discards a card from the hand.
/// </summary>
public class DiscardCard : Effect
{
    public new string Name = "Discard";
    //Text that appears in the description box
    public override void DoEffect()
    {
        Debug.Log("Discarding card");
        Debug.LogWarning("LANDY SEMPAI PLEASE FULFILL ME!!!!");
    }

// TODO: Add a use effects on death proc ( deathrattle )
}
