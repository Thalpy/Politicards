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
    public string location = "None";
    //the length of days the Crisis has
    public int DayLength;
    //The minimum progress level required to win the crisis
    public int minProgress = 5;
    //The name of the resultnames
    internal int activeTurns = 0;
    [SerializeField]
    public List<TriggerEffect> triggerEffectsStr = new List<TriggerEffect>();
    //The actual effect and trigger objects
    [SerializeField] internal Dictionary<Effect, Trigger> triggerEffects;
    internal Dictionary<Faction, int> factionProgress;
    //ProgressUpdateEvent event
    public ProgressUpdateEvent progressUpdateEvent = new ProgressUpdateEvent();
    [SerializeField]
    public List<Dialogue> dialogues = new List<Dialogue>();

    [SerializeField]
    public List<EndCrisis> endCrisis = new List<EndCrisis>();

    //awake
    public void Awake()
    {
        //Prevents a bug
        if(minProgress <= 0)
        {
            minProgress = 1;
        }
    }

    //copy the crisis to a new crisis
    public Crisis Copy()
    {
        Crisis newCrisis = new Crisis();
        newCrisis.Name = Name;
        newCrisis.Description = Description;
        newCrisis.image = image;
        newCrisis.audio = audio;
        newCrisis.DayLength = DayLength;
        newCrisis.minProgress = minProgress;
        newCrisis.endCrisis = endCrisis;
        newCrisis.dialogues = dialogues;

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
        //sends an event signal to ProgressUpdateEvent
        progressUpdateEvent.Invoke(GetProgress(), minProgress);

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
            triggerObj.setVars(effectObj, trigEff.triggerVars);
            effects.Add(effectObj, triggerObj);
        }
        triggerEffects = effects;
    }



    /// <summary>
    /// Checks if the crisis is active and if it is, it will end it
    /// Sends off flags for the result then removes it from the active crisis list
    /// </summary>
    public void EndCrisis()
    {
        if(!GameMaster.crisisMaster.isActiveCrisis(this)){
            Debug.LogError("Crisis " + Name + " is not active but something tried to stop it.");
            Debug.Break();
            return;
        }
        Faction victory = null;
        int victoryProgress = 0;
        foreach(KeyValuePair<Faction, int> entry in factionProgress){
            if(entry.Value > victoryProgress){
                victory = entry.Key;
                victoryProgress = entry.Value;
            }
        }
        if(victory == null){
            foreach (EndCrisis end in endCrisis)
            {
                if (end.faction == "")
                {
                    end.run();
                }
            }
        }
        
        foreach (EndCrisis end in endCrisis)
        {
            try{
                Faction faction = null;
                if (int.TryParse(end.faction, out int factionID))
                {
                    faction = GameMaster.factionController.SelectFaction(factionID);
                }
                else{
                    faction = GameMaster.factionController.SelectFaction(end.faction);
                }
                if (end.faction == victory.FactionName)
                {
                    end.run();
                }
            }
            catch(System.Exception e){
                Debug.Break();
                Debug.LogError("Error in EndCrisis: " + e.Message);
            }
        }

        // else{
        //     CheckTrigger($"{victory.FactionName}_Win");
        //     CheckTrigger($"Win");
        // }
        CheckTrigger("End");
        //safety check
        GameMaster.crisisMaster.RemoveCrisis(this);
    }

    public bool NewTurn(){
        activeTurns++;
        CheckTrigger("NewTurn");
        if(activeTurns >= DayLength){
            return false;
        }
        return true;
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

    public int[] GetProgress()
    {
        int[] progress = new int[factionProgress.Count];
        int i = 0;
        foreach (KeyValuePair<Faction, int> entry in factionProgress)
        {
            progress[i] = entry.Value;
            i++;
        }
        return progress;
    }

    public string SpeakProgress()
    {
        string progress = "";
        foreach (KeyValuePair<Faction, int> entry in factionProgress)
        {
            progress += $"{entry.Key.FactionName}: {entry.Value}/{minProgress}\n";
        }
        return progress;
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

[System.Serializable]
public class EndCrisis 
{
    public string faction = null;
    [SerializeField]
    public List<Dialogue> dialogues = new List<Dialogue>();
    [SerializeField]
    public List<TriggerEffect> triggerEffectsStr = new List<TriggerEffect>();

    public void run(){
        GameMaster.dialoguePlayer.StartDialogue(dialogues);
        foreach(TriggerEffect trigEff in triggerEffectsStr){
            Effect effectObj = GameMaster.GetEffect(trigEff.effectName).Copy();
            effectObj.setVars(this, trigEff.effectVars);
            effectObj.DoEffect();
        }
    }
}