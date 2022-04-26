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
        //convert args into a string list
        List<string> argsList = new List<string>();
        foreach (int arg in args)
        {
            argsList.Add(arg.ToString());
        }
        setVars(source, argsList);
    }

    public virtual void setVars(object source, List<string> args)
    {
        this.source = source;
        power = int.Parse(args[0]);
    }

    public virtual void setPower(int _power)
    {
        power = _power;
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

    /// <summary>
    /// Mostly an easy of use function to call the effect without copy pasting this code
    /// </summary>
    public virtual void AdjustProgress(Faction faction)
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
            GameMaster.playerHand.DrawCard();
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

    public override void setVars(object source, List<string> args)
    {
        this.source = source;
        power = int.Parse(args[0]);
        //if args[1] is can be an int
        if (int.TryParse(args[1], out int factionID))
        {
            faction = GameMaster.factionController.SelectFaction(factionID);
        }
        else
        {
            //if it's not an int, it's a string
            faction = GameMaster.factionController.SelectFaction(args[1]);
        }
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

    public override void setVars(object source, List<string> args)
    {
        this.source = source;
        power = int.Parse(args[0]);
        //if args[1] is can be an int
        if (int.TryParse(args[1], out int factionID))
        {
            faction = GameMaster.factionController.SelectFaction(factionID);
        }
        else
        {
            //if it's not an int, it's a string
            faction = GameMaster.factionController.SelectFaction(args[1]);
        }
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
    public override void setVars(object source, List<string> args)
    {
        this.source = source;
        power = int.Parse(args[0]);
        //if args[1] is can be an int
        if (int.TryParse(args[1], out int factionID))
        {
            faction = GameMaster.factionController.SelectFaction(factionID);
        }
        else
        {
            //if it's not an int, it's a string
            faction = GameMaster.factionController.SelectFaction(args[1]);
        }
    }

    public override void DoEffect()
    {
        AdjustProgress(faction);
    }
}

public class Double : Effect
{
    public Double()
    {
        name = "Double";
    }

    internal List<string> blackist = new List<string>();

    public override void setVars(object source, List<string> args){
        this.source = source;
        blackist = args;
    }

    public override void DoEffect()
    {
        //if source is a Card
        if (source is Card)
        {
            //get the card
            Card card = source as Card;
            //itterate through the card's triggerEffects
            Dictionary<Effect, Trigger> additions = new Dictionary<Effect, Trigger>();
            foreach(KeyValuePair<Effect, Trigger> efftrig in card.triggerEffects)
            {
                //if the effect is a double
                if (efftrig.Key.name == name && blackist.Contains(efftrig.Key.name))
                {
                    continue;
                }
                additions.Add(efftrig.Key.Copy(), efftrig.Value.Copy());
            }
            //add the new effects to the card
            foreach(var entry in additions)
            {
                card.triggerEffects.Add(entry.Key, entry.Value);
            }
        }
        //if source is a Crisis
        else if (source is Crisis)
        {
            //get the crisis
            Crisis crisis = source as Crisis;
            //itterate through the crisis's triggerEffects
            Dictionary<Effect, Trigger> additions = new Dictionary<Effect, Trigger>();
            foreach (KeyValuePair<Effect, Trigger> efftrig in crisis.triggerEffects)
            {
                //if the effect is a double
                if (efftrig.Key.name == name && blackist.Contains(efftrig.Key.name))
                {
                    continue;
                }
                additions.Add(efftrig.Key.Copy(), efftrig.Value.Copy());
            }
            //add the new effects to the crisis
            foreach (var entry in additions)
            {
                crisis.triggerEffects.Add(entry.Key, entry.Value);
            }           
        }
    }
}

public class MostPowerful : Effect
{
    public MostPowerful()
    {
        name = "MostPowerful";
    }
    Faction faction;
    public override void setVars(object source, List<string> args)
    {
        this.source = source;
        power = int.Parse(args[0]);
        //if args[1] is can be an int
        if (int.TryParse(args[1], out int factionID))
        {
            faction = GameMaster.factionController.SelectFaction(factionID);
        }
        else
        {
            //if it's not an int, it's a string
            faction = GameMaster.factionController.SelectFaction(args[1]);
        }
    }

    public override void DoEffect()
    {
        float highestPower = 0;
        float factionPower = 0;
        foreach(Faction _faction in GameMaster.factionController.GetFactions())
        {
            if (faction == _faction)
            {
                factionPower = _faction.FactionPower;
                continue;
            }
            if (_faction.FactionPower > highestPower)
            {
                highestPower = _faction.FactionPower;
            }
        }
        if(factionPower <= highestPower)
        {
            GameMaster.factionController.ChangeFactionPower(faction.FactionName, power);
        }
    }
}

public class BoostWeakest : Effect
{
    public BoostWeakest()
    {
        name = "BoostWeakest";
    }
    Faction faction;

    public override void DoEffect()
    {
        float lowestPower = 100;
        foreach (Faction _faction in GameMaster.factionController.GetFactions())
        {
            if (_faction.FactionPower < lowestPower)
            {
                faction = _faction;
            }
        }
        AdjustProgress(faction);
    }
}

public class Chaos :Effect
{
    public Chaos()
    {
        name = "Chaos";
    }

    public override void DoEffect()
    {
        //pick a random effect from the effect's list in GameMaster
        Effect effect = GameMaster.GetRandomEffect();
    }
}

public class AddCard : Effect{
    public AddCard()
    {
        name = "AddCard";
    }

    List<string> cardNames = new List<string>();
    string target;

    public override void setVars(object source, List<string> args)
    {
        this.source = source;
        target = args[0];
        cardNames = args.GetRange(1, args.Count - 1);
    }

    public override void DoEffect()
    {
        foreach(string cardname in cardNames)
        {
            Card card = GameMaster.cardMaster.getCard(cardname);
            if(target == "player")
            {
                GameMaster.playerHand.AddCard(card);
            }
            else if(target == "ai")
            {
                GameMaster.AISHand.AddCard(card);
            }
        } 
    }
}
