using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCrisis : Targetable
{
    public int index = 1;
    public bool player = true;
    public CrisisBox crisisBox;
    Vector3 startingScale;
    float bounceStrength = 0.1f;

    //awake
    void Awake()
    {
        GameMaster.AddTarget(gameObject);
        index = index -1;
        startingScale = transform.localScale;
    }
    public override bool DropCard(Card card)
    {
        Crisis crisis = crisisBox.GetCurrentCrisis();
        if(GameMaster.crisisMaster.CanAddCard(crisis, index))
        {
            #if DEBUG_TargetCrisis
            Debug.Log("In TargetCrisis.DropCard()");
            Debug.Log("Adding card to crisis: " + crisis.Name);
            #endif
            GameMaster.crisisMaster.ApplyCard(card, crisis, index, player);
            card.UseCard(crisis, index, player);
            GameMaster.cardMaster.makePsuedoCard(card, gameObject.transform);
            return true;
        }
        #if DEBUG_TargetCrisis
        Debug.LogWarning("Cannot add card to crisis: " + crisis.Name);
        #endif
        return false;        
    }

    public bool DropCardAI(Card card)
    {
        Crisis crisis = crisisBox.GetCurrentCrisis();
        if (GameMaster.crisisMaster.CanAddCard(crisis, index, false))
        {
            #if DEBUG_TargetCrisis
            Debug.Log("In TargetCrisis.DropCard()");
            Debug.Log("Adding card to crisis: " + crisis.Name);
            #endif
            //GameMaster.crisisMaster.ApplyCard(card, crisis, index, player);
            card.UseCard(crisis, index, player);
            GameMaster.cardMaster.makePsuedoCard(card, gameObject.transform);
            return true;
        }
        #if DEBUG_TargetCrisis
        Debug.Log("Cannot add card to crisis: " + crisis.Name);
        #endif
        return false;
    }

    //Update
    void Update(){
        //If mouse is currently held down and oh yeah it's hack time
        //If the gameobject has a child in unity
        if(player && Input.GetMouseButton(0) && transform.childCount == 0){
            //If the mouse is over the target
            Bounce();
        }
        else{
            transform.localScale = startingScale;
        }
    }

    //Gets the scale of the card and bounces it back and forth a bit
    public void Bounce(){
        float scale = transform.localScale.x;
        //randomly change the scale of the card
        float scaleChange = Random.Range(-bounceStrength,bounceStrength);
        transform.localScale = new Vector3(startingScale.x + scaleChange, startingScale.y + scaleChange, startingScale.z + scaleChange);
    }

}