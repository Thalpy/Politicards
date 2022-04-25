using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This class literally a pretend card just to look like there's a card there working away.
/// </summary>
public class PsuedoCard : MonoBehaviour {
    Card card;
    public TextMeshPro Title;
    public TextMeshPro Description;
    public SpriteRenderer image;

    public PsuedoCard(Card card, Vector3 position) {
        SetUp(card, position);
    }

    public void SetUp(Card card, Vector3 position) {
        this.card = card;
        transform.position = position;
        Title.text = card.Name;
        Description.text = card.Description;
        image.sprite = card.image;
    }
}