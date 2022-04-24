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

    [SerializeField] GameObject Deck;
    [SerializeField] GameObject Discard;
    

    [SerializeField] public Vector3 DeckOffScreenLocation;
    [SerializeField] public Vector3 DiscardOffScreenLocation;


    [SerializeField] int MaxHandSize;
    [SerializeField] int StartingDeckSize;

    [SerializeField] bool PlayersHand;

    [SerializeField] public List<GameObject> Targets;

    [SerializeField] GameObject MouseOverText;
    [SerializeField] Vector3 MouseOverTextDefaultPosition;

    
    void Start()
    {

    for (int i = 0; i < GameMaster.cardMaster.Decks[0].cards.Count; i++) //Initalizes the deck, in future this will be done from a predermined list of cards based on character choice.
    {
        
        CardsInDeck.Add(Instantiate(Card,DeckOffScreenLocation,transform.rotation,Deck.transform));
        JL_CardController CC =  CardsInDeck[i].GetComponent<JL_CardController>();
        CardsInDeck[i].name = "Card " + i;
        CC.Deck = Deck;
        CC._Card = GameMaster.cardMaster.Decks[0].cards[i];
        CC.Discard = Discard;
        CC.Hand = gameObject;
        CC.Position = DeckOffScreenLocation;
    }

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
                while(CardsInHand.Count < MaxHandSize)
                {
                    
                    DrawCard();
                }             
                }

            }
        }

        if (Input.GetMouseButtonDown(0))
        {

            if (Physics.Raycast(_Ray, out _Hit, 200))
            {
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
        Debug.Log(CardIndex);
        
        
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
        DrawOrder = CardIndex;


        Debug.Log(new Vector3(x,y,z));
        return (new Vector3(x,y,z));
    }

    public void DrawCard() //This function is how cards are moved from the deck to the hand, it is in hand controller because it is a triggered by the 
    {
        
        if (CardsInDeck.Count == 0)
        {
            CardsInDeck = CardsInDiscard;
            
            for (int i = 0; i < CardsInDeck.Count; i++)
            {
                CardsInDeck[i].transform.position = DeckOffScreenLocation;
                CardsInDeck[i].GetComponent<JL_CardController>().Position = DeckOffScreenLocation;
                CardsInDeck[i].transform.SetParent(Deck.transform);
            }

            CardsInDiscard.Clear();
            ShuffleDeck();
        }

        if (CardsInDeck.Count != 0)
        {
            CardsInHand.Add(CardsInDeck[0]);

            CardsInDeck[0].transform.position = Deck.transform.position;
            int DrawOrder;
            CardsInDeck[0].GetComponent<JL_CardController>().Position = GetCardHandPosition(CardsInDeck[0], out DrawOrder);
            CardsInDeck[0].GetComponent<SpriteRenderer>().sortingOrder = DrawOrder;

            if (PlayersHand)
            {
                CardsInDeck[0].GetComponent<JL_CardController>().InHand = true; 
            }
            else
            {
                CardsInDeck[0].GetComponent<JL_CardController>().InAdversariesHand = true; 
            }

            CardsInDeck[0].transform.SetParent(gameObject.transform);

        
           
            CardsInDeck.Remove(CardsInDeck[0]);

        }


    }

    public void ShuffleDeck() // randomly moves cards into a new list and then randomly back again.
    {
        float x = CardsInDeck.Count;
        
        List<GameObject> TempDeck = new List<GameObject>();
        
        for (int i = 0; i < x; i++)
        {
            int j = Random.Range(0,CardsInDeck.Count);
            
            TempDeck.Add(CardsInDeck[j]);
            CardsInDeck.Remove(CardsInDeck[j]);
        }

        for (int i = 0; i < x; i++)
        {
            int j = Random.Range(0,TempDeck.Count);
            
            CardsInDeck.Add(TempDeck[j]);
            TempDeck.Remove(TempDeck[j]);
        }
    }


}
