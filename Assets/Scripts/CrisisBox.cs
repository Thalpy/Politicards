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
    //name text
    public TextMeshProUGUI nameText;
    //description text
    public TextMeshProUGUI descriptionText;
    //image
    public Image image;
    //audio effect player
    public AudioSource audioSource;




    //Feeling variable might comment later
    /// <summary>
    /// This function changes the display values to the passed in crisis.
    /// </summary>
    public void ChangeEvent(Crisis SussyCrisis)
    {
        //set the name
        nameText.text = SussyCrisis.Name;
        //set the description
        descriptionText.text = SussyCrisis.Description;
        //set the image
        image.sprite = SussyCrisis.image;
        //play the sound
        audioSource.clip = SussyCrisis.audio;
        audioSource.Play();
    }
}
