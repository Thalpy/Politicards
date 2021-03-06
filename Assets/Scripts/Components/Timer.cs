using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// This is a timer component that can be used to track time.
/// </summary>
[System.Serializable]
public class Timer
{
    //The length of the timer
    private int length;
    public int timeLeft;
    // The function to call at the end of the timer
    public Action callback;

    private Buff buff;

    /// <summary>
    /// Creates a timer object with the given length.
    /// length: the length of the timer in seconds
    /// func: the function to call when the timer ends
    /// </summary>
    public Timer(int length, Action endFunc, Action startFunc = null, string hovertext = null, string imagefile = null)
    {
        this.length = length;
        timeLeft = length;
        callback = endFunc;
        GameMaster.AddTimer(this);
        if(hovertext != null && imagefile != null){
            buff = GameMaster.AddBuff(hovertext, imagefile);
        }
        if(startFunc != null){
            startFunc();
        }
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

    public int checkTimer()
    {
        return timeLeft;
    }

    /// <summary>
    /// Ends the timer and calls the callback function.
    /// Also removes the object from the GameMaster's list of timers.
    /// </summary>
    public void EndTimer()
    {
        //TODO: JOE FIX PLZ
        //callback();
        //GameMaster.RemoveTimer(this);
        //destroy the timer
        //Destroy(this);
    }

    public void StopAndKillTimer()
    {
        timeLeft = 0;
        EndTimer();
    }
}