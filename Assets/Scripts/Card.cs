using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to store the functionality of a card.
/// This is just a data class, so don't add specific functionality here.
/// Instead use the Effects.cs class to add effects to it's use.
/// </summary>
[System.Serializable] //makes it so that cards can be edited in the inspector
public class Card
{
    //name text
    public string Name;
    //description text
    public string Description;
    //image
    public string ImageName;
    //mana cost
    public int ManaCost;
    //faction
    public string Faction;
    //effects that happen when the card is played
    public List<string> EffectNames;

    //converts imagename to sprite
    public Sprite GetImage()
    {
        //convert imagename in to a sprite
        return Resources.Load<Sprite>("Images/" + ImageName);
    }

    /// <summary>
    /// converts EffectNames into Effects
    /// You will need to call DoEffect() on the result to actually get it to do anything though!
    /// </summary>
    public List<Effect> GetEffects()
    {
        List<Effect> effects = new List<Effect>();
        foreach (string effectName in EffectNames)
        {
            effects.Add(GameMaster.GetEffect(effectName));
        }
        return effects;
    }
}

