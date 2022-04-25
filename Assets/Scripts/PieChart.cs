using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//TO DO:
//  - Rewrite the code to rotate by 90 degrees - it looks like the detection is not working correctly


/// <summary>
/// The Pie Chart Controller
/// use this script to control the pie chart
/// call the setValues function to update the pie chart.
/// </summary>

[System.Serializable]
public struct PieChartData
{
    public int index;
    public string name;

    public Image image;

    public float power;

    public float lowerBoundingAngle;

    public float upperBoundingAngle;

    //constructor
    public PieChartData(int index, string name, Image image, float power, float lowerBoundingAngle, float upperBoundingAngle)
    {
        this.index = index;
        this.name = name;
        this.image = image;
        this.power = power;
        this.lowerBoundingAngle = lowerBoundingAngle;
        this.upperBoundingAngle = upperBoundingAngle;
    }


}



public class PieChart : MonoBehaviour
{

    // an event to be called if the mouse is over a slice
    public delegate void OnMouseOverSlice(string FactionName);
    public static event OnMouseOverSlice onMouseOverSlice;

    /// <summary>
    /// A reference to the game master, set in editor
    /// </summary>
    [SerializeField] GameMaster GameMaster;

    [SerializeField] Vector2 mousePosition;

    [SerializeField] Color colourUnderMouse;

    [SerializeField] float totalPower;


    /// <summary>
    /// The list of party pie sectors.
    /// [0] = People, [1] = Economic, [2] = Military, [3] = Nobility, [4] = Crime
    /// </summary>
    public Image[] imagesPieChart;

    /// <summary>
    /// The list of party power proportions.
    /// [0] = People, [1] = Economic, [2] = Military, [3] = Nobility, [4] = Crime
    /// </summary>
    public float[] values;

    [SerializeField] public PieChartData[] pieChartData;



    /// <summary>
    /// Given the list of party power proportions, this method will calculate the chart segment fill amount.
    /// </summary>
    /// <param name="values">
    /// The list of party power proportions.
    /// [0] = People, [1] = Economic, [2] = Military, [3] = Nobility, [4] = Crime
    /// </param>
    public void SetValues(float[] values)
    {
        //for each value in values, set the power of the pieChartData element with the same index
        for (int i = 0; i < values.Length; i++)
        {
            //look for the element with the same index as the current value
            for (int j = 0; j < pieChartData.Length; j++)
            {
                //if the element has the same index as the current value
                if (pieChartData[j].index == i)
                {
                    //set the power of the element to the current value
                    pieChartData[j].power = values[i];
                }
            }
        }

        // get the total power of all the pie chart elements then set this.totalPower to this value
        totalPower = 0;
        for (int i = 0; i < pieChartData.Length; i++)
        {
            totalPower += pieChartData[i].power;
        }


        // declare a variable to store the total fill amount
        float totalFillAmount = 0;

        // for each element in the pie chart, calculate the fill amount as power / total power,  add it to the total fill amount and set the fill amount of the element
        for (int i = 0; i < pieChartData.Length; i++)
        {
            totalFillAmount = totalFillAmount + (pieChartData[i].power / totalPower);
            imagesPieChart[i].fillAmount = totalFillAmount;
        }      
       

        // for each element of the pieChartData array, set the lower bounding angle to the sum of the previous elements' upper bounding angles unless it is the first element in which case it is 0. set the upper bounding angle equal to the fill amount of the current element.
        for (int i = 0; i < pieChartData.Length; i++)
        {
            if (i == 0)
            {
                pieChartData[i].lowerBoundingAngle = 0;
            }
            else
            {
                pieChartData[i].lowerBoundingAngle = pieChartData[i - 1].upperBoundingAngle;
            }
            pieChartData[i].upperBoundingAngle = pieChartData[i].image.fillAmount;
        }
    }

    /// <summary>
    /// Calculates the percentage of the total value occupied by the party at the given index.
    /// </summary>
    /// <param name="valuesToSet">
    /// The list of party power proportions.
    /// [0] = People, [1] = Economic, [2] = Military, [3] = Nobility, [4] = Crime
    /// </param>
    /// <param name="index">
    /// The index of the party in the list.
    /// </param>
    /// <returns></returns>
    public float GetPercentage(float[] valuesToSet, int index)
    {
        float totalValue = 0;

        for (int i = 0; i < valuesToSet.Length; i++)
        {
            totalValue += valuesToSet[i];
        }
        float percentage = valuesToSet[index] / totalValue;

        //get the piechart data for the party at the given index
        PieChartData pieChartData = this.pieChartData[index];

        pieChartData.upperBoundingAngle = percentage;


        return percentage;

    }



    //start
    void Start()
    {
        //FactionController factionController = GameMaster.GetComponent<FactionController>();
        //get the faction power
        //float[] factionPower = factionController.GetFactionPower();
        //set the values
        //SetValues(factionPower);
        //set the values each to 20
        SetValues(new float[] { 20, 20, 20, 20, 20 });

        foreach(Faction faction in GameMaster.factionController.GetFactions())
        {
            //we subscribe to the faction power change event on all factions to automatically update the pie chart at run time.
            faction.factionPowerChange.AddListener(OnFactionPowerChange);
        }



    }

    void Update()
    {

        //go through the pie chart data. if the index is 0 then set the lower bounding angle to 0. otherwise set it to the upper bounding angle of the previous pie chart data.
        for (int i = 0; i < pieChartData.Length; i++)
        {
            if (i == 0)
            {
                pieChartData[i].lowerBoundingAngle = 0;
            }
            else
            {
                pieChartData[i].lowerBoundingAngle = pieChartData[i - 1].upperBoundingAngle;
            }
        }


    }


    // a function that takes in a float between 0 and 1, and returns the pieChartData where the float is between the lower and upper bounding angles
    public string GetPieChartData(float value)
    {

        //write the value to the console
        Debug.Log($"Get Pie Chart Data recieeved a value of {value}");

        for (int i = 0; i < pieChartData.Length; i++)
        {
            if (value >= pieChartData[i].lowerBoundingAngle && value <= pieChartData[i].upperBoundingAngle)
            {
                return pieChartData[i].name;
            }
        }
        return pieChartData[0].name;
    }

    public void OnFactionPowerChange()
    {
        Debug.Log("Faction Power Change Event Fired");
        float[] factionPower = GameMaster.factionController.GetFactionPower();
        SetValues(factionPower);
    }

    // a function called testPieChart which calls the set values function passing in an array of 5 random floats between 0 and 100
    public void TestPieChart()
    {
        //create an array of 5 random floats between 0 and 100
        float[] values = new float[5];
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = Random.Range(0, 100);
        }
        //set the values
        SetValues(values);
    }






}


