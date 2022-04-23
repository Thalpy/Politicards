using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to couple an effect to a trigger.
/// This is just a data object, so don't add functionality here.
/// </summary>
[System.Serializable]
public class TriggerEffect{
    public string effectName;
    public int effectPower = 1;
    public string triggerName;
    public int triggerPower = 1;
}