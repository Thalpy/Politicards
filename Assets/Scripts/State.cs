using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for a state.
/// </summary>
public abstract class State : MonoBehaviour
{
    public abstract State RunCurrentState();
}