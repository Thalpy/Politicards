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
    public string ImageName;
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
    //effects that happen when the card is played
    [SerializeField]
    public List<TriggerEffect> triggerEffectsStr = new List<TriggerEffect>();
    //The actual effect and trigger objects
    internal Dictionary<Effect, Trigger> triggerEffects;

    /// <summary>
    /// Should be called whenever a card is put into play
    /// </summary>
    public void DrawCard(){
        SetUpEffects();
    }

    //copy the card to a new card
    public Card Copy()
    {
        Card newCard = new Card();
        newCard.Name = Name;
        newCard.Description = Description;
        newCard.ImageName = ImageName;
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
            Effect effectObj = GameMaster.GetEffect(trigEff.effectName).Copy();
            effectObj.setPower(trigEff.effectPower);
            Trigger triggerObj = GameMaster.GetTrigger(trigEff.triggerName).Copy();
            triggerObj.setPower(trigEff.triggerPower);
            effects.Add(effectObj, triggerObj);
        }
        triggerEffects = effects;
    }

    //converts imagename to sprite
    public Sprite GetImage()
    {
        //convert imagename in to a sprite
        return Resources.Load<Sprite>("Images/" + ImageName);
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
}

