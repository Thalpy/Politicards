using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public object source;
    internal List<Action<int>> actions;
    public virtual void setVars(object source, params int[] args)
    {
        this.source = source;
        power = args[0];
    }

    public virtual void addAction(Action<int> func){
        actions.Add(func);
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

//IN PROGRESS
public class Power : Effect
{
    public Power()
    {
        name = "Power";
    }

    public Faction faction;

    public override void setVars(object source, params int[] args)
    {
        base.setVars(args[0]);
        GameMaster.factionController.SelectFaction(args[1]);
    }

    public override void DoEffect()
    {
        GameMaster.factionController.ChangeFactionPower(faction.FactionName, power);
    }
}

public class Happiness : Effect
{
    public Happiness()
    {
        name = "Happiness";
    }

    public Faction faction;

    public override void setVars(object source, params int[] args)
    {
        base.setVars(args[0]);
        GameMaster.factionController.SelectFaction(args[1]);
    }

    public override void DoEffect()
    {
        GameMaster.factionController.ChangeFactionHappiness(faction.FactionName, power);
    }
}

public class Progress : Effect
{
    public Progress()
    {
        name = "Progress";
    }

    public Faction faction;

    /// <summary>
    /// Sets the power [0] and faction [1] variables 
    /// </summary>
    public override void setVars(object source, params int[] args)
    {
        base.setVars(args[0]);
        GameMaster.factionController.SelectFaction(args[1]);
    }

    public override void DoEffect()
    {
        //if the source is a card
        if (source is Card)
        {
            //get the card
            Card card = source as Card;
            //find the card in the active crisises
            Crisis crisis = GameMaster.crisisMaster.FindCrisisFromCard(card);
            //alter the crisis progress
            crisis.AdjustProgress(power, faction);
        }
    }
}
