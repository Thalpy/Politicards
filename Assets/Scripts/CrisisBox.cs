using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This class is used to display a crisis on the screen.
/// </summary>
public class CrisisBox : MonoBehaviour
{
    public string location;
    //name text
    public TextMeshPro nameText;
    //description text
    public TextMeshPro descriptionText;
    //image
    public SpriteRenderer image;
    //audio effect player
    public AudioSource audioSource;
    //progressChart
    public ProgressChart progressChart;
    //active crisis
    public Crisis crisis;

    private void Awake() {
        //Add this to the master list
        GameMaster.crisisMaster.crisisBoxes.Add(this);
        GameMaster._JL_EventMover.AddEvent(gameObject);
    }

    //on click event
    public void OnMouseDown()
    {
        //if the crisis is active
        if (crisis == null || gameObject.active == false)
        {
            Debug.LogWarning("Crisis not active when clicked on!!");
            return;
        }
        if(GameMaster._JL_EventMover.IsActive(gameObject)){
            GameMaster._JL_EventMover.SetInactive(gameObject);            
        }
        else{
            GameMaster._JL_EventMover.SetSingleActive(gameObject);
        }
        
    }

    //Feeling variable might comment later
    /// <summary>
    /// This function changes the display values to the passed in crisis.
    /// </summary>
    public void ChangeEvent(Crisis SussyCrisis)
    {
        crisis = SussyCrisis;
        //set the name
        nameText.text = SussyCrisis.Name;
        //set the description
        descriptionText.text = SussyCrisis.Description;
        //set the image
        image.sprite = SussyCrisis.image;
        //play the sound
        audioSource.clip = SussyCrisis.audio;
        audioSource.Play();
        progressChart.SetUpChart(SussyCrisis);
        gameObject.SetActive(true);
    }

    public void EndCrisis()
    {
        crisis = null;
        gameObject.SetActive(false);
    }

    public Crisis GetCurrentCrisis(){
        return crisis;
    }
}
