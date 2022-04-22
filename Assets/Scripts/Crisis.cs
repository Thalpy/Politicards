using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to store a crisis with a list of effects.
/// It is ONLY a data object, so don't add functionality here.
/// </summary>
[System.Serializable] //makes it so that the crisis can be edited in the inspector
public class Crisis
{
    //Name that appears at the top
    public string Name;
    //Text that appears in the description box
    public string Description;
    //the name of the image to use
    public string ImageName;
    //the length of days the Crisis has
    public int DayLength;
    //The name of the resultnames
    [SerializeField]
    public List<string> EffectNames;

    //Conversion from string to result

    public Sprite GetImage()
    {
        //convert imagename in to a sprite
        return Resources.Load<Sprite>("Images/" + ImageName);
    }

    public List<Effect> GetEffects()
    {
        //convert EffectNames into Effects
        List<Effect> results = new List<Effect>();
        foreach (string effectName in EffectNames)
        {
            results.Add(GameMaster.GetEffect(effectName));
        }
        return results;
    }
}
