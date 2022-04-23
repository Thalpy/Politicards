using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Old buddyboi GameMaster is back again to bring us static objects to call to.
/// This lets us call functions from the GM without having to pass the GM as a parameter.
/// Basically usable code everywhere, just call GameMaster to get it.
/// </summary>
public class GameMaster : MonoBehaviour
{
    public static CrisisMaster crisisMaster;
    public static JL_HandController handController;
    public static CardMaster cardMaster;
    public List<Effect> effects = new List<Effect>();

    
    // Start is called before the first frame update
    void Awake()
    {
        crisisMaster = GetComponent<CrisisMaster>();
        cardMaster = GetComponent<CardMaster>();
        //Feel free to clean this up Landy
        //get the hand gameobject
        GameObject _Hand = GameObject.Find("Hand");
        handController = _Hand.GetComponent<JL_HandController>();
        //create a list of all classes that inherit from the Effect class
        effects.AddRange(System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Effect)))
            .Select(type => (Effect)System.Activator.CreateInstance(type)));


        

    }
    //TODO
    public static Effect GetEffect(string name)
    {
        //TODO get results from gamemaster
        // foreach (Effect result in GameMaster.Effects)
        // {
        //     if (result.Name == name)
        //     {
        //         return result;
        //     }
        // }
        return null;
    }
}