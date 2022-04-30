using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardMaster))]
public class CardMasterEditor : Editor
{
    // on pressing a button calls the SendRandomMana method
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CardMaster myScript = (CardMaster)target;
        if (GUILayout.Button("Test all cards"))
        {
            foreach(FactionDeck list in myScript.Decks){
                foreach(Card card in list.cards){
                    card.DrawCard();
                }
            }
        }
    }
}
