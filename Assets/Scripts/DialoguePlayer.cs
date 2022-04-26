using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialoguePlayer : MonoBehaviour{
    public List<Dialogue> activeDialogues;
    public AudioSource audioSource;
    public TMPro.TextMeshProUGUI text;
    //unity image component
    public UnityEngine.UI.Image image;
    internal string revealedText = "";
    internal string targetText = "";
    internal int index;
    public float textDelay = 0.01f;
    internal float time;
    internal float oldTime;

    public void Update(){
        //If the user clicks on the box
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            if(targetText == revealedText){
                ProgressDialogue();
            }    
            else
            {
                revealedText = targetText;
                text.text = revealedText;
            }
        }
        
        //Scroll the text over time       
        if(targetText != revealedText && oldTime < time){
            RevealText(index);
            index++;
            oldTime = Time.time + textDelay;
        }
        time = Time.time;
    }

    //reveal text word by word progressively
    public bool RevealText(int index, int speed = 1){
        if(index < targetText.Length){
            revealedText = targetText.Substring(0, index);
            text.text = revealedText;
            return false;
        }
        else{
            revealedText = targetText;
            text.text = revealedText;
            return true;
        }
    }


    public void StartDialogue(List<Dialogue> dialogues){
        CopyDialogue(dialogues);
        gameObject.SetActive(true);
        //set the first dialogue
        ProgressDialogue();
    }

    public void CopyDialogue(List<Dialogue> dialogues){
        activeDialogues = new List<Dialogue>();
        foreach(Dialogue d in dialogues){
            activeDialogues.Add(d.Copy());
        }
    }

    //update the shown text and etc
    public void LoadDialogue(Dialogue dialogue){
        //set the text
        targetText = dialogue.text;
        revealedText = "";
        index = 0;
        //set the image
        image.sprite = dialogue.image;
        //set the audio
        audioSource.clip = dialogue.audio;
        //play the audio
        audioSource.Play();
    }

    //Progress the dialogue by one
    public void ProgressDialogue(){
        //if there is no dialogue, return
        if(activeDialogues.Count == 0){
            EndDialogue();
        }
        //if the dialogue is not done, continue
        Dialogue dialogue = activeDialogues[0];
        LoadDialogue(dialogue);
        activeDialogues.RemoveAt(0);
    }

    //Ends the dialogue
    public void EndDialogue(){
        gameObject.SetActive(false);
    }


}

[System.Serializable]
public class Dialogue{
    public string text;
    public AudioClip audio;
    public Sprite image;

    //Copy
    public Dialogue Copy(){
        Dialogue d = new Dialogue();
        d.text = text;
        d.audio = audio;
        d.image = image;
        return d;
    }
}