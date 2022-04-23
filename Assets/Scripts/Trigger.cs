using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to store the functionality of an effect.
/// </summary>
public abstract class Trigger
{
    //name 
    public string Name = "ERROR"; //name setter

    //Function that is called on the event box ending
    public abstract void CheckTrigger();
    //Function that is called on setup
    public abstract void SetupTrigger();
}

public class TimeOutTrigger : Trigger
{
    public new string Name = "TimeOut";
    public float TimeOut = 0; //time out setter
    public float TimeOutTimer = 0; //time out timer setter

    public override void SetupTrigger()
    {
        throw new System.NotImplementedException();
    }

    public override void CheckTrigger()
    {
        TimeOutTimer += Time.deltaTime;
        if (TimeOutTimer >= TimeOut)
        {
            TimeOutTimer = 0;
            //TriggerEvent();
        }
    }
}