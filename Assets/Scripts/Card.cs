using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to store the functionality of a card.
/// This is just a data class, so don't add specific functionality here.
/// Instead use the Effects.cs class to add effects to it's use.
/// </summary>
[System.Serializable] //makes it so that cards can be edited in the inspector
public class Card
{
    //name text
    public string Name;
    //description text
    public string Description;
    //image
    public Sprite image;
    //audio sound effect
    public AudioClip audio;
    //mana cost
    public int ManaCost;

    public float FactionPowerIncrease;

    public float MilitarySupportIncrease;

    public float EconomicSupportIncrease;

    public float NobilitySupportIncrease;

    public float PeopleSupportIncrease;

    public float ProgressIncrease;
    //faction
    public string Faction;
    // Is in starting deck
    public bool StartingCard;
    //An array of power Value for the factions
    [SerializeField]
    public List<int> PowerValues = new List<int>();
    //An array of happiness values for the factions
    [SerializeField]
    public List<int> HappinessValues = new List<int>();
    //An array of progress Value for the Crisis
    [SerializeField]
    public List<int> ProgressValues = new List<int>();
    //effects that happen when the card is played
    [SerializeField]
    public List<TriggerEffect> triggerEffectsStr = new List<TriggerEffect>();
    //The actual effect and trigger objects
    public Dictionary<Effect, Trigger> triggerEffects;

    /// <summary>
    /// Should be called whenever a card is put into play
    /// </summary>
    public void DrawCard(){
        SetUpEffects();
        CheckTrigger("Draw");
    }

    public void UseCard(Crisis crisis, int index, bool player = true){

        GameMaster.crisisMaster.ApplyCard(this, crisis, index, player);
        CheckTrigger("Use");
    }

    //copy the card to a new card
    public Card Copy()
    {
        Card newCard = new Card();
        newCard.Name = Name;
        newCard.Description = Description;
        newCard.image = image;
        newCard.ManaCost = ManaCost;
        newCard.Faction = Faction;
        newCard.triggerEffects = new Dictionary<Effect, Trigger>(triggerEffects);
        return newCard;
    }

    public void SetUpEffects()
    {
        Dictionary<Effect, Trigger> effects = new Dictionary<Effect, Trigger>();
        foreach (TriggerEffect trigEff in triggerEffectsStr)
        {
            if(trigEff.effectName == ""){
                Debug.Log("TriggerEffect has no effect name in crisis " + Name + "Replacing with nothing.");
                continue;
            }
            Effect effectObj = GameMaster.GetEffect(trigEff.effectName).Copy();
            effectObj.setVars(this, (List<string>)trigEff.effectVars);
            Trigger triggerObj = GameMaster.GetTrigger(trigEff.triggerName).Copy();
            triggerObj.setVars(effectObj, trigEff.triggerVars);
            effects.Add(effectObj, triggerObj);
        }
        Trigger onUse = GameMaster.GetTrigger("OnUse").Copy();
        //Powervalues setup
        for (int i = 0; i < PowerValues.Count; i++)
        {
            if (PowerValues[i] == 0)
            {
                continue;
            }
            Effect powerEffect = GameMaster.GetEffect("Power").Copy();
            powerEffect.setVars(this, PowerValues[i], i);
            effects.Add(powerEffect, onUse);
        }
        //Happinessvalues setup
        
        for (int i = 0; i < HappinessValues.Count; i++)
        {
            if (HappinessValues[i] == 0)
            {
                continue;
            }
            Effect happinessEffect = GameMaster.GetEffect("Happiness").Copy();
            happinessEffect.setVars(this, HappinessValues[i], i);

            effects.Add(happinessEffect, onUse);
        }

        //Progressvalues setup
        for (int i = 0; i < ProgressValues.Count; i++)
        {
            if (ProgressValues[i] == 0)
            {
                continue;
            }
            Effect progressEffect = GameMaster.GetEffect("Progress").Copy();
            progressEffect.setVars(this, ProgressValues[i], i);
            effects.Add(progressEffect, onUse);
        }

        triggerEffects = effects;
    }

    //converts imagename to sprite
    public Sprite GetImage()
    {
        //convert imagename in to a sprite
        return Resources.Load<Sprite>("Images/" + image);
    }


    /// <summary>
    /// Gets a list of events that should happen when the input trigger is true
    /// </summary>
    public List<Effect> GetEffects(string trigger)
    {
        //convert EffectNames into Effects
        List<Effect> results = new List<Effect>();
        foreach (TriggerEffect trigEff in triggerEffectsStr)
        {
            if (trigEff.triggerName == trigger)
            {
                results.Add(GameMaster.GetEffect(trigEff.effectName));
            }
        }
        return results;
    }

    public void CheckTrigger(string trigger)
    {
        foreach (KeyValuePair<Effect, Trigger> effect in triggerEffects)
        {
            if (effect.Value.CheckTrigger(trigger))
            {
                effect.Key.DoEffect();
            }
        }
    }
}

[System.Serializable]
public class SerializableInt
{
    public int value;
    public bool hasValue;
}

