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

    public virtual void setVars(List<string> args)
    {
        power = int.Parse(args[0]);
    }

    public virtual void setVars(params int[] args)
    {
        power = args[0];
    }

    public virtual void SetupTrigger(Effect _effect, int _power)
    {
        effect = _effect;
        setVars(_power);
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
    public override void setVars(List<string> args){
        return;
    }
    //Function that is called on setup
    public override void SetupTrigger(Effect _effect, int _power){
        _effect.DoEffect();
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

    public override void SetupTrigger(Effect _effect, int _power)
    {
        effect = _effect;
        power = _power;
        timer = new Timer(power, _effect.DoEffect);
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

    public override void SetupTrigger(Effect _effect, int _power)
    {
        effect = _effect;
        power = _power;
    }

    public void ActivateTrigger(){
        effect.DoEffect();
    }
}