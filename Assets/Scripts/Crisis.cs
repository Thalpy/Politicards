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
    public Sprite image;
    //The name of the audioclip to play
    public AudioClip audio;
    //the length of days the Crisis has
    public int DayLength;
    //The name of the resultnames
    [SerializeField]
    public List<TriggerEffect> triggerEffectsStr = new List<TriggerEffect>();
    //The actual effect and trigger objects
    internal Dictionary<Effect, Trigger> triggerEffects;
    internal Dictionary<Faction, int> factionProgress;

    //copy the crisis to a new crisis
    public Crisis Copy()
    {
        Crisis newCrisis = new Crisis();
        newCrisis.Name = Name;
        newCrisis.Description = Description;
        newCrisis.image = image;
        newCrisis.audio = audio;
        newCrisis.DayLength = DayLength;
        if (triggerEffects != null){
            newCrisis.triggerEffects = new Dictionary<Effect, Trigger>(triggerEffects);
        }
        else{
            newCrisis.triggerEffectsStr = new List<TriggerEffect>(triggerEffectsStr);
        }
        return newCrisis;
    }

    public void AdjustProgress(int power, Faction faction){
        //get the faction from the factionProgress dictionary and add the power
        factionProgress[faction] += power;
    }

    //Conversion from string to result
    public void StartCrisis()
    {
        SetUpEffects();
        CheckTrigger("Start");
        factionProgress = new Dictionary<Faction, int>();
        //get the list of factions
        List<Faction> factions = GameMaster.factionController.GetFactions();
        //for each faction
        foreach (Faction faction in factions)
        {
            //add the faction to the dictionary
            factionProgress.Add(faction, 0);
        }
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
            effectObj.setVars(this, trigEff.effectVars);
            Trigger triggerObj = GameMaster.GetTrigger(trigEff.triggerName).Copy();
            triggerObj.setVars(trigEff.triggerVars);
            effects.Add(effectObj, triggerObj);
        }
        triggerEffects = effects;
    }

    public void EndCrisis()
    {
        CheckTrigger("End");
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

    // public Sprite GetImage()
    // {
    //     //convert image in to a sprite
    //     return Resources.Load<Sprite>("Sprites/" + image);
    // }

    // //gets the AudioClip to play
    // public AudioClip GetAudio()
    // {
    //     return Resources.Load<AudioClip>("Audio/" + audio);
    // }

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
