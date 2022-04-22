using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Result
{


    //name 
    public string Name(string data); //name setter

    public void Name(); // name getter
    //Function that is called on the event box ending
    public void DoResult();
}

//example child of results
public class DrawCard : Result
{
    public string Name { get; set; } = "Draw card";
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
