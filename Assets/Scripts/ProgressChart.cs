using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to store the functionality of a card.
/// This is just a data class, so don't add specific functionality here.
/// Instead use the Effects.cs class to add effects to it's use.
/// </summary>
public class ProgressChart : MonoBehaviour{
    public SpriteRenderer peopleBar;
    public SpriteRenderer economicBar;
    public SpriteRenderer militaryBar;
    public SpriteRenderer nobilityBar;
    public Crisis crisis;
    //SIGNALING TIMEEEEEEE
    
    private void Start() {
        foreach(Faction f in GameMaster.factionController.GetFactions()) {
            if(f.FactionName == "People") {
                peopleBar.color = f.FactionColor;
            }
            if(f.FactionName == "Economic") {
                economicBar.color = f.FactionColor;
            }
            if(f.FactionName == "Military") {
                militaryBar.color = f.FactionColor;
            }
            if(f.FactionName == "Nobility") {
                nobilityBar.color = f.FactionColor;
            }
        }
        crisis.progressUpdateEvent.AddListener(onProgressUpdate);
    }

    public void SetUpChart(Crisis crisis) {
        this.crisis = crisis;
        UpdateProgress(crisis.GetProgress(), crisis.minProgress);
    }

    public void onProgressUpdate(int[] progress, int minProgress) { 
        UpdateProgress(progress, minProgress);
    }

    public void Resize(Transform transform, float amount, Vector3 direction)
    {
        float prev_scale = transform.localScale.y;
        //calculate the difference
        float diff = amount - prev_scale;
        transform.position += direction * amount / 2; // Move the object in the direction of scaling, so that the corner on ther side stays in place
        transform.localScale += direction * amount; // Scale object in the specified direction
    }

    //Scales the bars depending on the current progress
    //a scale size of 8 matches the minProgress of the crisis
    public void UpdateProgress(int[] progress, int minProgress) {
        //floor scale to be at least 1
        float scale = Mathf.Max(1, (float)progress[0] / (float)minProgress);
        Resize(peopleBar.transform, scale, Vector3.up);
        scale = Mathf.Max(1, (float)progress[1] / (float)minProgress);
        Resize(economicBar.transform, scale, Vector3.up);
        scale = Mathf.Max(1, (float)progress[2] / (float)minProgress);
        Resize(militaryBar.transform, scale, Vector3.up);
        scale = Mathf.Max(1, (float)progress[3] / (float)minProgress);
        Resize(nobilityBar.transform, scale, Vector3.up);
    }
}