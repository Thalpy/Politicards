using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    //Name that appears at the top
    string Name;
    //Text that appears in the description box
    string Description;
    //The image used for the center of the panel
    Sprite Image;
    //the length of days the event has
    int DayLength;
    //the results possible
    List<Result> Results;
}
