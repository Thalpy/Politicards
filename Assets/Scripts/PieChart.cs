using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


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

    public float lowerBoundingAngle;

    public float upperBoundingAngle;

    //constructor
    public PieChartData(int index, string name, float lowerBoundingAngle, float upperBoundingAngle)
    {
        this.index = index;
        this.name = name;
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

    [SerializeField] public PieChartData[] pieChartData = new PieChartData[5] {
        new PieChartData(0, "People", 0, 0),
        new PieChartData(1, "Economic", 0, 0),
        new PieChartData(2, "Military", 0, 0),
        new PieChartData(3, "Nobility", 0, 0),
        new PieChartData(4, "Crime", 0, 0)
    };



    /// <summary>
    /// Given the list of party power proportions, this method will calculate the chart segment fill amount.
    /// </summary>
    /// <param name="values">
    /// The list of party power proportions.
    /// [0] = People, [1] = Economic, [2] = Military, [3] = Nobility, [4] = Crime
    /// </param>
    public void SetValues(float[] values)
    {
        float totalValue = 0;
        for (int i = 0; i < values.Length; i++)
        {
            totalValue += GetPercentage(values, i);
            imagesPieChart[i].fillAmount = totalValue;

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
        float percentage =  valuesToSet[index] / totalValue;

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



    


}


