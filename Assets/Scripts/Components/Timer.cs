using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// This is a timer component that can be used to track time.
/// </summary>
public class Timer : MonoBehaviour
{
    //The length of the timer
    private int length;
    public int timeLeft;
    // The function to call at the end of the timer
    public Action callback;

    /// <summary>
    /// Creates a timer object with the given length.
    /// length: the length of the timer in seconds
    /// func: the function to call when the timer ends
    /// </summary>
    public Timer(int length, Action func)
    {
        this.length = length;
        timeLeft = length;
        callback = func;
        GameMaster.AddTimer(this);
    }

    /// <summary>
    /// Increases the timer by 1 turn.
    /// </summary>
    public void increase_turn()
    {
        timeLeft--;
        if (timeLeft <= 0)
        {
            EndTimer();
        }
    }

    /// <summary>
    /// Ends the timer and calls the callback function.
    /// Also removes the object from the GameMaster's list of timers.
    /// </summary>
    public void EndTimer()
    {
        callback();
        GameMaster.RemoveTimer(this);
        //destroy the timer
        Destroy(this);
    }
}