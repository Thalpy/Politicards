using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to store a crisis with a list of effects.
/// It is ONLY a data object, so don't add functionality here.
/// </summary>
[System.Serializable] //makes it so that the crisis can be edited in the inspector
public class Crisis
{
    //Name that appears at the top
    public string Name;
    //Text that appears in the description box
    public string Description;
    //the name of the image to use
    public string ImageName;
    //the length of days the Crisis has
    public int DayLength;
    //The name of the resultnames
    [SerializeField]
    public List<TriggerEffect> triggerEffectsStr = new List<TriggerEffect>();
    //The actual effect and trigger objects
    internal Dictionary<Effect, Trigger> triggerEffects;

    //copy the crisis to a new crisis
    public Crisis Copy()
    {
        Crisis newCrisis = new Crisis();
        newCrisis.Name = Name;
        newCrisis.Description = Description;
        newCrisis.ImageName = ImageName;
        newCrisis.DayLength = DayLength;
        newCrisis.triggerEffects = new Dictionary<Effect, Trigger>(triggerEffects);
        return newCrisis;
    }

    //Conversion from string to result
    public void StartCrisis()
    {
        SetUpEffects();
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
    
    //Checks each of the trigger effects to see if the trigger is true and if so calls the effect
    public void CheckTriggers()
    {
        foreach (KeyValuePair<Effect, Trigger> effect in triggerEffects)
        {
            if (effect.Value.CheckTrigger())
            {
                effect.Key.DoEffect();
            }
        }
    }

    public void CheckTrigger(string trigger = null)
    {
        foreach (KeyValuePair<Effect, Trigger> effect in triggerEffects)
        {
            if (effect.Value.CheckTrigger(trigger))
            {
                effect.Key.DoEffect();
            }
        }
    }

    public Sprite GetImage()
    {
        //convert imagename in to a sprite
        return Resources.Load<Sprite>("Images/" + ImageName);
    }

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
