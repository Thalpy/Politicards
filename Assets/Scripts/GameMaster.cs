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
    public static List<Timer> timers = new List<Timer>();
    public static int turn = 0;
    //internal List<Timer> timers = new List<Timer>();


    public PieChart pieChart;

    
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
    // increases turn by 1
    public static void NextTurn()
    {
        turn++;
        foreach(Timer timer in timers)
        {
            timer.increase_turn();
        }
    }

    //adds a timer to the list of timers
    public static void AddTimer(Timer timer)
    {
        timers.Add(timer);
    }
    
    //removes a timer from the list of timers
    public static void RemoveTimer(Timer timer)
    {
        timers.Remove(timer);
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

    public void TestPieChart()
    {   
        //create an array of 5 random floats between 0 and 100
        float[] randomFloats = new float[5];
        for (int i = 0; i < randomFloats.Length; i++)
        {
            randomFloats[i] = Random.Range(0f, 100f);
        }

        //call the pieChart.SetValues function with the array as a parameter
        pieChart.SetValues(randomFloats);

    }
}