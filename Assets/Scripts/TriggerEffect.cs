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
    [SerializeField]
    public List<string> effectVars = new List<string>();
    public string triggerName;
    public List<string> triggerVars = new List<string>();
}