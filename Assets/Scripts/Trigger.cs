using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to store the functionality of an effect.
/// </summary>
[System.Serializable]
public abstract class Trigger
{
    //name 
    public string name = "ERRORTRIGGER"; //name setter
    internal Effect effect;
    //The variable power of the trigger (type dependant)
    public int power;

    public virtual void setVars(Effect _effect, List<string> args)
    {
        effect = _effect;
        if(args.Count > 0)
        {
            power = int.Parse(args[0]);
        }
        SetupTrigger();
    }

    public virtual void setVars(Effect _effect, params int[] args)
    {
        effect = _effect;
        if(args.Length > 0)
        {
            power = args[0];
        }
        SetupTrigger();
    }

    public virtual void SetupTrigger()
    {
        return;
    }

    public virtual bool CheckTrigger(string _triggerName = null){
        return true;
    }

    //Copies this object into a new one
    public virtual Trigger Copy()
    {
        //creates a new object of the same type
        return System.Activator.CreateInstance(this.GetType()) as Trigger;
    }
}
public class Instant : Trigger
{
    public Instant()
    {
        name = "Instant";
    }
    public override void setVars(Effect _effect, List<string> args){
        _effect.DoEffect();
        return;
    } 
}

/// <summary>
/// Triggers when it's used.
/// </summary>
public class OnUse : Trigger
{
    public OnUse()
    {
        name = "OnUse";
    }
    //Function that is called on setup
    public override bool CheckTrigger(string _triggerName = null){
        if(_triggerName == name){
            return true;
        }
        return false;
    }    
}

public class TimeOutTrigger : Trigger
{
    public TimeOutTrigger()
    {
        name = "TimeOut";
    }
    public Timer timer;

    public override void SetupTrigger()
    {
        timer = new Timer(power, effect.DoEffect);
    }

    public override bool CheckTrigger(string _triggerName)
    {
        //Timer triggering is handled in the timer class
        //This is a debug check basically
        int timeLeft = timer.checkTimer();
        if (timeLeft > 0){
            Debug.Log("Timer is still running and has " + timeLeft + " turns left");
            return false;
        }
        else
        {
            Debug.LogWarning("Timer is done, You shouldn't be seeing this!!");
            return true;
        }
    }
}

public class UnhappyTrigger : Trigger{
    public UnhappyTrigger()
    {
        name = "UnhappyTrigger";
    }

    public override void SetupTrigger()
    {
        
    }

    public void ActivateTrigger(){
        effect.DoEffect();
    }
}


public class Friendly : Trigger{
    public Friendly()
    {
        name = "Friendly";
    }

    Faction faction;

    public override void setVars(Effect _effect, List<string> args)
    {
        effect = _effect;
        faction = GameMaster.factionController.SelectFaction(int.Parse(args[0]));
        SetupTrigger();
    }

    public override void SetupTrigger()
    {
        
    }

    public void ActivateTrigger(){
        if(faction == null){
            Debug.LogError("Faction is null");
            return;
        }
        if(faction.PlayerHappiness == Faction.PlayerHappinessEnum.happy){
            effect.DoEffect();
        }
    }
}


public class Investment : Trigger{
    public Investment()
    {
        name = "Investment";
    }

    public override bool CheckTrigger(string _triggerName = null){
        if(_triggerName == name){
            return true;
        }
        return false;
    }
}

 public class VictoryTrigger : Trigger{
    public VictoryTrigger()
    {
        name = "Victory";
    }

    Faction faction;

    public override  void setVars(Effect _effect, List<string> args){
        //try parsing args[0] as an int
        if(int.TryParse(args[0], out int factionID)){
            faction = GameMaster.factionController.SelectFaction(int.Parse(args[0]));
        }
        else{
            faction = GameMaster.factionController.SelectFaction(args[0]);
        }
    }

    public override bool CheckTrigger(string _triggerName = null){
        string check = faction.FactionName + "_Win";
        if(_triggerName == name){
            return true;
        }
        return false;
    }
 }

  public class WinTrigger : Trigger{
    public WinTrigger()
    {
        name = "Win";
    }

    Faction faction;

    public override  void setVars(Effect _effect, List<string> args){
        return;
    }

    public override bool CheckTrigger(string _triggerName = null){
        if(_triggerName == name){
            return true;
        }
        return false;
    }
 }

 public class LoseTrigger : Trigger{
    public LoseTrigger()
    {
        name = "Lose";
    }

 }

 public class End : Trigger{
    public End()
    {
        name = "End";
    }

    public override bool CheckTrigger(string _triggerName = null){
        if(_triggerName == name){
            return true;
        }
        return false;
    }
 }