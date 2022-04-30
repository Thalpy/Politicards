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
    public SpriteRenderer image;
    public GameObject dialogueBox;

    [SerializeField]
    public Dictionary<string, List<Dialogue>> Scenes  = new Dictionary<string, List<Dialogue>>();
    internal string revealedText = "";
    internal string targetText = "";
    internal int index;
    public float textSpeed = 0.05f;
    internal float time;
    [SerializeField] public static List<Dialogue> WinningDialogue = new List<Dialogue>();

    public void Awake(){
        GameMaster.dialoguePlayer = this;
    }

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
        if(targetText != revealedText && time > textSpeed){
            RevealText(index);
            index++;
            time = 0;
        }
        time = time + Time.deltaTime;
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


    public void StartDialogueFromScene(string name){
        if(Scenes.ContainsKey(name)){
            StartDialogue(Scenes[name]);
        }
        else{
            Debug.LogError("Dialogue " + name + " not found!");
        }
    }

    public void StartDialogue(List<Dialogue> dialogues){
        CopyDialogue(dialogues);
        dialogueBox.SetActive(true);
        //set the first dialogue
        ProgressDialogue();
    }

    public void CopyDialogue(List<Dialogue> dialogues){
        //activeDialogues = new List<Dialogue>();
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
            return;
        }
        //if the dialogue is not done, continue
        Dialogue dialogue = activeDialogues[0];
        LoadDialogue(dialogue);
        activeDialogues.RemoveAt(0);
    }

    //Ends the dialogue
    public void EndDialogue(){
        dialogueBox.SetActive(false);
    }


    public void EndGame(){
        StartDialogue(WinningDialogue);
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