using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the wider card functionality methods
/// As well as providing the means to input new card data
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class CardMaster : MonoBehaviour
{
    [SerializeField]
    public List<FactionDeck> Decks = new List<FactionDeck>();
    public GameObject dummyCard;
    //Card sound effect player
    public AudioSource cardAudio;
    //backup sound
    public AudioClip backupSound;

    //awake
    void Awake()
    {
        cardAudio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Converts a string to an card object
    /// </summary>
    public Card getCard(string cardName)
    {
        foreach (FactionDeck faction in Decks)
        {
            foreach (Card card in faction.cards)
            {
                if (card.Name == cardName)
                {
                    return card;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Makes a pseduo card from a card
    /// </summary>
    public GameObject makePsuedoCard(Card card, Transform transform)
    {
        //creates a new dummyCard
        GameObject newCard = Instantiate(dummyCard, transform.position, Quaternion.identity);
        newCard.transform.parent = transform;
        newCard.transform.rotation = new Quaternion(0, 0, 0, 0);
        //gets the psudoCard script
        newCard.GetComponent<PsuedoCard>().SetUp(card, transform.position);
        newCard.transform.localScale = new Vector3(1, 1, 1);
        return newCard;
    }
}

/// <summary>
/// This class is used to store a faction with a list of cards.
/// It is ONLY a data object, so don't add functionality here.
/// </summary>
[System.Serializable]
public class FactionDeck
{
    public string faction;
    [SerializeField]
    public List<Card> cards = new List<Card>();
}