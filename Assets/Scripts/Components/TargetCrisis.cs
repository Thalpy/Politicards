using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCrisis : Targetable
{
    public int index = 1;
    public bool player = true;
    public CrisisBox crisisBox;

    //awake
    void Awake()
    {
        GameMaster.AddTarget(gameObject);
        index = index -1;
    }
    public override bool DropCard(Card card)
    {
        Crisis crisis = crisisBox.GetCurrentCrisis();
        if(GameMaster.crisisMaster.CanAddCard(crisis, index))
        {
            GameMaster.crisisMaster.ApplyCard(card, crisis, index, player);
            card.UseCard(crisis, index, player);
            GameMaster.cardMaster.makePsuedoCard(card, gameObject.transform);
            return true;
        }
        return false;        
    }
}