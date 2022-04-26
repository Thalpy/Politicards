using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerRelationshipEnum
{
    Neutral,
    Ally,
    Enemy
}

/// <summary>
/// Abstract enum to make it less stressful to keep track of the factions
/// </summary>
public enum FactionEnum
{
    People,
    Economic,
    Military,
    Nobility,
    Crime
}