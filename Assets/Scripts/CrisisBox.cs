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
    public TextMeshPro timeLeftText;
    //image
    public SpriteRenderer image;
    //audio effect player
    public AudioSource audioSource;
    //progressChart
    public ProgressChart progressChart;
    //active crisis
    public Crisis crisis;
    public List<GameObject> Psuedos = new List<GameObject>(); 

    private void Start() {
        //Add this to the master list
        GameMaster.crisisMaster.crisisBoxes.Add(this);
        GameMaster._JL_EventMover.AddEvent(gameObject);
        EndCrisis();
    }

    private void Update() {
        if(crisis != null){
            timeLeftText.text = (crisis.DayLength - crisis.activeTurns).ToString();
        }
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
        EndCrisis();
        gameObject.SetActive(true);
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
    }

    public void AddPseudo(GameObject psudeocard){
        if(psudeocard == null){
            Debug.LogWarning("Cannot add null psudeocard to crisis box");
            return;
        }
        Psuedos.Add(psudeocard);
    }

    public void EndCrisis()
    {
        //crisis = null;

        gameObject.SetActive(false);
        //for loop for psudeo cards

        List<GameObject> cachedList = new List<GameObject>(Psuedos);
        //loop over
        foreach (GameObject psudeocard in cachedList)
        {
            if(psudeocard != null){
                psudeocard.SetActive(false);
            }
            //remove from list
            Psuedos.Remove(psudeocard);
            //destroy
            Destroy(psudeocard);
        }
        GameMaster.ClearTargetsOfCards(this);
    }

    public Crisis GetCurrentCrisis(){
        return crisis;
    }
}
