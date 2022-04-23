using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Faction : MonoBehaviour
{
    //private string factionName with getter and setter
    [SerializeField] private string factionName;
    public string FactionName
    {
        get { return factionName; }
        set { factionName = value; }
    }

    //private int factionPower with getter and setter
    [SerializeField] private int factionPower;
    public int FactionPower
    {
        get { return factionPower; }
        set { factionPower = value; }
    }

    //a private float that represents the faction's current happiness with the player
    [SerializeField] private float factionHappiness;
    public float FactionHappiness
    {
        get { return factionHappiness; }
        set { factionHappiness = value; }
    }

    //a private float that represents the faction's current happiness with the AI  
    [SerializeField] private float factionAiHappiness;
    public float FactionAiHappiness
    {
        get { return factionAiHappiness; }
        set { factionAiHappiness = value; }
    }

    //a function to change the faction's happiness
    public void ChangeHappiness(float amount)
    {
        factionHappiness += amount;
    }

    //a function to change the faction's happiness with the ai
    public void ChangeAiHappiness(float amount)
    {
        factionAiHappiness += amount;
    }

    //a function to change the faction's power
    public void ChangePower(int amount)
    {
        factionPower += amount;
    }






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
