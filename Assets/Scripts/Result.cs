using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Result
{
    //name 
    string Name { get; }
    //Function that is called on the event box ending
    public void DoResult();
}

//example child of results
public class DrawCard : Result
{
    string Name = "Draw Card";
    //Text that appears in the description box
    public void DoResult()
    {
        Debug.Log("Drawing card");
    }
}

public class DiscardCard : Result
{
    string Name = "Discard Card";
    //Text that appears in the description box
    public void DoResult()
    {
        Debug.Log("Discarding card");
    }
}
