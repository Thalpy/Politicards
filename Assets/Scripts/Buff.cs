using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff{
    public string name;
    public string image;
    public string hovertext;
    public Buff(string name, string image, string hovertext, int duration, bool is_permanent){
        this.name = name;
        this.image = image;
        this.hovertext = hovertext;
    }
}