using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCrisis : Targetable
{
    public int index = 1;
    public bool player = true;
    public CrisisBox crisisBox;
    public override void DropCard(Card card)
    {
        card.UseCard(crisisBox.GetCurrentCrisis(), index, player);
    }
}