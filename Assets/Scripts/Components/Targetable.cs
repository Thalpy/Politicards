using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a targetable component that lets cards be applied to it.
/// </summary>
[System.Serializable]
public class Targetable : MonoBehaviour
{
    public float allowedDistance =  5f;
    void Start()
    {
        GameMaster.AddTarget(gameObject);
    }

    public virtual bool DropCard(Card card)
    {
        return false;
    }
}

