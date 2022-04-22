using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Result
{
    //name 
    public string Name = "ERROR"; //name setter

    //Function that is called on the event box ending
    public virtual void DoResult(){
        Debug.LogWarning("Attempted to call a result that doesn't exist!!");
    }
}

//example child of results
public class DrawCard : Result
{
    public new string Name = "Draw card";
    //Text that appears in the description box
    public override void DoResult()
    {
        Debug.Log("Draw a card");
    }
}

public class DiscardCard : Result
{
    public new string Name = "Discard Card";
    //Text that appears in the description box
    public override void DoResult()
    {
        Debug.Log("Discarding card");
    }
}
