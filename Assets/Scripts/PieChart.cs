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

    // a function that detects whether the mouse is over the pie chart
    public bool IsMouseOver()
    {
        Vector2 mousePos = Input.mousePosition;
        RectTransform rect = GetComponent<RectTransform>();
        Vector2 localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, mousePos, null, out localPos);
        return rect.rect.Contains(localPos);
    }

    // a function to get the color of the pixel under the mouse
    public Color GetPixelColor(Vector2 mousePos)
    {
        {
            //capture the current screen to a texture
            Texture2D tex = new Texture2D(Screen.width, Screen.height);
            tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            //read the colour of the pixel at the mouse position from texture
            Color color = tex.GetPixel((int)mousePos.x, (int)mousePos.y);
            return color;
        }
        
    }

    //start 
        void Start()
        {
            //FactionController factionController = GameMaster.GetComponent<FactionController>();
            //get the faction power
            //float[] factionPower = factionController.GetFactionPower();
            //set the values
            //SetValues(factionPower);

        }

        void Update()
        {
            // if the mouse is over the pie chart
            // check the color of the pixel under the mouse.
            // based upon the pixel colour, get the faction name
            // call the onMouseOverSlice event
            if (IsMouseOver())
            {
                Vector2 mousePos = Input.mousePosition;
                Color color = GetPixelColor(mousePos);
                //write the colour to the console
                Debug.Log(color);
            }

           // mousePosition = Input.mousePosition;
            //colourUnderMouse = GetPixelColor(mousePosition);



        }
}


