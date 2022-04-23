using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Old buddyboi GameMaster is back again to bring us static objects to call to.
/// This lets us call functions from the GM without having to pass the GM as a parameter.
/// Basically usable code everywhere, just call GameMaster to get it.
/// </summary>

//require a crisis master, a card master, a faction controller and a hand controller to be present
[RequireComponent(typeof(CrisisMaster))]
[RequireComponent(typeof(CardMaster))]
[RequireComponent(typeof(FactionController))]
public class GameMaster : MonoBehaviour
{
    public static CrisisMaster crisisMaster;
    public static JL_HandController handController;
    public static CardMaster cardMaster;

    public static FactionController factionController;

    //Change these to internal after debugging
    [SerializeField]
    public static List<Effect> effects = new List<Effect>();
    [SerializeField]
    public static List<Trigger> triggers = new List<Trigger>();
    [SerializeField]
    public static List<Timer> timers = new List<Timer>();
    public static int turn = 0;
    //internal List<Timer> timers = new List<Timer>();


    /// <summary>
    /// a reference to the pieChart
    /// </summary>
    public PieChart pieChart;
    
    // Start is called before the first frame update
    void Awake()
    {
        crisisMaster = GetComponent<CrisisMaster>();
        cardMaster = GetComponent<CardMaster>();
        factionController = GetComponent<FactionController>();

        //LANDY TODO:
        // Add a reference to the hand controller here in a way that doesn't use GameObject.Find

        //get the hand gameobject
        //GameObject _Hand = GameObject.Find("Hand");
        //handController = _Hand.GetComponent<JL_HandController>();
        //create a list of all classes that inherit from the Effect class
        effects.AddRange(System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Effect)))
            .Select(type => (Effect)System.Activator.CreateInstance(type)));
        //create a list of all classes that inherit from the Trigger class
        triggers.AddRange(System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Trigger)))
            .Select(type => (Trigger)System.Activator.CreateInstance(type)));
        foreach(Effect e in effects)
        {
            Debug.Log(e.name);
        }
        foreach(Trigger t in triggers)
        {
            Debug.Log(t.name);
        }
    }

    //returns a copy of effects
    public List<Effect> GetEffects()
    {
        List<Effect> _effects = new List<Effect>();
        foreach(Effect e in effects)
        {
            _effects.Add(e.Copy());
        }
        return _effects;
    }

    //returns a copy of triggers
    public List<Trigger> GetTriggers()
    {
        List<Trigger> _triggers = new List<Trigger>();
        foreach(Trigger t in triggers)
        {
            _triggers.Add(t.Copy());
        }
        return _triggers;
    }

    internal static Buff AddBuff(string hovertext, string imagefile)
    {
        throw new System.NotImplementedException();
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
        
        foreach(Effect effect in effects)
        {
            if(effect.name == name)
            {
                return effect;
            }
        }
        Debug.LogError("Effect " + name + " not found!!");
        return null;
    }

    public static Trigger GetTrigger(string name)
    {
        foreach(Trigger trigger in triggers)
        {
            if(trigger.name == name)
            {
                return trigger;
            }
        }
        Debug.LogError("Trigger " + name + " not found!!");
        return null;
    }

    /// <summary>
    ///  Sets the power of each faction to a random value between 0 and 100 and updates the pie chart
    /// </summary>
    public void TestPieChart()
    {   
        //create an array of 5 random floats between 0 and 100
        

        //get an array of the faction names from the faction controller
        string[] factionNames = factionController.GetFactionNames();    
        //for each faction name, call the set power by name function on the faction controller, passing in the faction name and a random float between 0 and 100
        foreach (string factionName in factionNames)
        {
            factionController.ChangeFactionPower(factionName, Random.Range(-100, 100));

        } 

        //get the faction power array from the faction controller
        float[] factionPower = factionController.GetFactionPower();
        //update the pie chart with the faction power array
        pieChart.SetValues(factionPower);

    }

    public static void CheckTrigger(string trigger){
        foreach (Crisis crisis in crisisMaster.crisises){
            crisis.CheckTrigger(trigger);
        }
    }
}