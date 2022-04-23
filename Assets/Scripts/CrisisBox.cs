using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to display a crisis on the screen.
/// </summary>
public class CrisisBox : MonoBehaviour
{
    //name text
    public TextMesh NameText;
    //description text
    public TextMesh DescriptionText;
    //image
    public SpriteRenderer Image;
    //results
    public List<Effect> Effects;


    //Feeling variable might comment later
    /// <summary>
    /// This function changes the display values to the passed in crisis.
    /// </summary>
    void ChangeEvent(Crisis SussyCrisis)
    {
        //set the name
        NameText.text = SussyCrisis.Name;
        //set the description
        DescriptionText.text = SussyCrisis.Description;
        //set the image
        Image.sprite = SussyCrisis.GetImage();
        //set the results
        Effects = SussyCrisis.GetEffects();
    }
}
