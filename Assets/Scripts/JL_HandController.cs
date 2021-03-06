using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JL_HandController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Card;
    [SerializeField] public List<GameObject> CardsInHand;
    [SerializeField] public List<GameObject> CardsInDeck;
    [SerializeField] public List<GameObject> CardsInDiscard;

    [SerializeField] float MaxHandWidth;
    [SerializeField] float MaxCardSpacing;
    [SerializeField] float HandWidth;
    [SerializeField] int startingCards = 4;

    [SerializeField] GameObject Deck;
    [SerializeField] GameObject Discard;
    

    [SerializeField] public Vector3 DeckOffScreenLocation;
    [SerializeField] public Vector3 DiscardOffScreenLocation;


    [SerializeField] int MaxHandSize;
    [SerializeField] int StartingDeckSize;

    [SerializeField] bool PlayersHand;



    [SerializeField] GameObject MouseOverText;
    [SerializeField] Vector3 MouseOverTextDefaultPosition;



    
    void Start()
    {

        if (PlayersHand)
        {
            GameMaster.playerHand = gameObject.GetComponent<JL_HandController>();
        }
        else
        {
            GameMaster.AISHand = gameObject.GetComponent<JL_HandController>();
        }


        //for (int startingCard = 0; startingCard < startingCards; startingCard++)
        //{
            // for (int cards = 0; cards < GameMaster.cardMaster.Decks[0].cards.Count; cards++) //Initalizes the deck, in future this will be done from a predermined list of cards based on character choice.
            // {
                
            //     CardsInDeck.Add(Instantiate(Card,DeckOffScreenLocation,transform.rotation,Deck.transform));
            //     JL_CardController CC =  CardsInDeck[cards].GetComponent<JL_CardController>();
            //     CardsInDeck[cards].name = "Card " + cards*GameMaster.cardMaster.Decks[0].cards.Count;
            //     CC.Deck = Deck;
            //     CC._Card = GameMaster.cardMaster.Decks[0].cards[cards*GameMaster.cardMaster.Decks[0].cards.Count];
            //     CC.Discard = Discard;
            //     CC.Hand = gameObject;
            //     CC.Position = DeckOffScreenLocation;
            // }
        //}
        
        foreach(Card card in GameMaster.cardMaster.Decks[0].cards)
        {
            AddCard(card);
        }
        for (int startingCard = 0; startingCard <= startingCards; startingCard++)
        {
            DrawCard(false);
        }
    }



    public GameObject AddCard(Card _Card)
    {
        GameObject cardObj = Instantiate(Card, DeckOffScreenLocation, transform.rotation, Deck.transform);
        CardsInDeck.Add(cardObj);
        JL_CardController CC = cardObj.GetComponent<JL_CardController>();
        CC.Deck = Deck;
        CC._Card = _Card.Copy();
        CC.Discard = Discard;
        CC.Hand = gameObject;
        CC.Position = DeckOffScreenLocation;
        return cardObj;
    }

    void Update() // These Update functions are just for Debugging and can be removed from the final build, clicking on the deck draws cards and clicking ont he discard discards them.
    {
        
        Ray _Ray;
        RaycastHit _Hit;
        _Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Input.GetMouseButtonDown(0))
        {
            
            

            if (Physics.Raycast(_Ray, out _Hit, 200))
            {
                if (_Hit.transform == Deck.transform)
                {
                    
                    DrawCard();
            
                }
                if (_Hit.transform == Discard.transform)
                {

                    
                    CardsInHand[CardsInHand.Count-1].GetComponent<JL_CardController>().DiscardAction();
           
                }

            }
        }





        if (Physics.Raycast(_Ray, out _Hit, 200) && PlayersHand)
        {
            JL_MouseOverText _JL_MouseOverText = _Hit.transform.gameObject.GetComponent<JL_MouseOverText>();
            if (_JL_MouseOverText != null)      
            {

            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;
            float z = 50;
            MouseOverText.transform.position = Camera.main.ScreenToWorldPoint(new Vector3 (x,y,z));
            MouseOverText.GetComponent<TMPro.TextMeshPro>().text = _Hit.transform.gameObject.GetComponent<JL_MouseOverText>().GetMouseOverText();
            }

        }
        else if(PlayersHand)
        {
        MouseOverText.transform.position = MouseOverTextDefaultPosition;
        }
    }

    

    
    public Vector3 GetCardHandPosition(GameObject Card, out int DrawOrder) // This function returns the hand position for the card.
    {
        
        int CardIndex = 0;

        for (int i = 0; i < CardsInHand.Count; i++)
        {
            if(CardsInHand[i] == Card)
            {
                CardIndex = i;
            }
        }
        //Debug.Log(CardIndex);
        
        
        HandWidth = Mathf.Min(MaxHandWidth,MaxCardSpacing*CardsInHand.Count);

        float x;
        if (CardsInHand.Count == 1)
        {
        x = gameObject.transform.position.x;
        }
        else
        {
        x = CardIndex*HandWidth/(CardsInHand.Count-1) + gameObject.transform.position.x - HandWidth/2;
        }
        
        
        float y = gameObject.transform.position.y;
        float z = gameObject.transform.position.z;
        DrawOrder = CardIndex*10+20;


        //Debug.Log(new Vector3(x,y,z));
        return (new Vector3(x,y,z));
    }

    public void DrawCard(bool shuffle = true) //This function is how cards are moved from the deck to the hand, it is in hand controller because it is a triggered by the LANDY WHY
    {
        
        if (CardsInDeck.Count == 0)
        {
            
            for (int i = 0; i < CardsInDiscard.Count; i++)
            {
                CardsInDiscard[i].transform.position = DeckOffScreenLocation;
                CardsInDiscard[i].GetComponent<JL_CardController>().Position = DeckOffScreenLocation;
                CardsInDiscard[i].transform.SetParent(Deck.transform);
                CardsInDeck.Add(CardsInDiscard[i]);
            }

            CardsInDiscard.Clear();
            ShuffleDeck();
        }

        if (CardsInDeck.Count != 0)
        {
            GameObject transitionCard = CardsInDeck[0];
            JL_CardController cardController = transitionCard.GetComponent<JL_CardController>();
            CardsInHand.Add(transitionCard);
            cardController.Discarded = false;
            cardController._Card.DrawCard();

            transitionCard.transform.position = Deck.transform.position;
            int DrawOrder;
            transitionCard.GetComponent<JL_CardController>().Position = GetCardHandPosition(transitionCard, out DrawOrder);
            transitionCard.GetComponent<SpriteRenderer>().sortingOrder = DrawOrder;

            if (PlayersHand)
            {
                transitionCard.GetComponent<JL_CardController>().InHand = true; 
            }
            else
            {
                transitionCard.GetComponent<JL_CardController>().InAdversariesHand = true; 
            }

            transitionCard.transform.SetParent(gameObject.transform);
            CardsInDeck.Remove(transitionCard);            
        }
        if(shuffle)
        {
            ShuffleDeck();
        }
    }

    public void ShuffleDeck() // This function shuffles the deck.
    {
        for (int i = 0; i < CardsInDeck.Count; i++)
        {
            int randomIndex = Random.Range(i, CardsInDeck.Count);
            GameObject temp = CardsInDeck[randomIndex];
            CardsInDeck[randomIndex] = CardsInDeck[i];
            CardsInDeck[i] = temp;
        }
    }

    public void AddSpecificCardToHand(GameObject transitionCard) // This function is used to add a specific card to the hand.
    {
        JL_CardController cardController = transitionCard.GetComponent<JL_CardController>();
        CardsInHand.Add(transitionCard);
        cardController._Card.DrawCard();

        transitionCard.transform.position = Deck.transform.position;
        int DrawOrder;
        transitionCard.GetComponent<JL_CardController>().Position = GetCardHandPosition(transitionCard, out DrawOrder);
        transitionCard.GetComponent<SpriteRenderer>().sortingOrder = DrawOrder;

        if (PlayersHand)
        {
            transitionCard.GetComponent<JL_CardController>().InHand = true; 
        }
        else
        {
            transitionCard.GetComponent<JL_CardController>().InAdversariesHand = true; 
        }

        transitionCard.transform.SetParent(gameObject.transform);
        CardsInDeck.Remove(transitionCard);         
    }
    // public void ShuffleDeck() // randomly moves cards into a new list and then randomly back again.
    // {
    //     CardsInDeck.shu


    //     float x = CardsInDeck.Count;
        
    //     List<GameObject> TempDeck = new List<GameObject>();
        
    //     for (int i = 0; i < x; i++)
    //     {
    //         int j = Random.Range(0,CardsInDeck.Count);
            
    //         TempDeck.Add(CardsInDeck[j]);
    //         CardsInDeck.Remove(CardsInDeck[j]);
    //     }

    //     for (int i = 0; i < x; i++)
    //     {
    //         int j = Random.Range(0,TempDeck.Count);
            
    //         CardsInDeck.Add(TempDeck[j]);
    //         TempDeck.Remove(TempDeck[j]);
    //     }
    // }
}
