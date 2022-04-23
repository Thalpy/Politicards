using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// The Pie Chart Controller
/// use this script to control the pie chart
/// call the setValues function to update the pie chart.
/// </summary>

public class PieChart : MonoBehaviour
{

    /// <summary>
    /// The list of party pie sectors.
    /// [0] = People, [1] = Economic, [2] = Military, [3] = Nobility, [4] = Crime
    /// </summary>
    public Image[]  imagesPieChart;

    /// <summary>
    /// The list of party power proportions.
    /// [0] = People, [1] = Economic, [2] = Military, [3] = Nobility, [4] = Crime
    /// </summary>
    public float[] values;

    

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

        return valuesToSet[index] / totalValue;
        
    }


    // Start is called before the first frame update
    void Start()
    {
        SetValues(values);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
