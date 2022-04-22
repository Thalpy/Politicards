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

    
    void Start()
    {

    for (int i = 0; i < StartingDeckSize; i++) //Initalizes the deck, in future this will be done from a predermined list of cards based on character choice.
    {
        CardsInDeck.Add(Instantiate(Card,DeckOffScreenLocation,transform.rotation,Deck.transform));
        JL_CardController CC =  CardsInDeck[i].GetComponent<JL_CardController>();
        CardsInDeck[i].name = "Card " + i;
        CC.Deck = Deck;
        CC.Discard = Discard;
        CC.Hand = gameObject;
        CC.Position = DeckOffScreenLocation;
    }

    }

    void Update() // These Update functions are just for Debugging and can be removed from the final build, clicking on the deck draws cards and clicking ont he discard discards them.
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray _Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _Hit;

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
            Ray _Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _Hit;

            if (Physics.Raycast(_Ray, out _Hit, 200))
            {
                if (_Hit.transform == Discard.transform)
                {

                    
                    CardsInHand[CardsInHand.Count-1].GetComponent<JL_CardController>().DiscardAction();
           
                }

            }
        }      
    }

    

    
    public Vector3 GetCardHandPosition(GameObject Card) // This function returns the hand position for the card.
    {
        
        int CardIndex = 0;

        for (int i = 0; i < CardsInHand.Count; i++)
        {
            if(CardsInHand[i].name == Card.name)
            {
                CardIndex = i;
            }
        }
        

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
        float z = gameObject.transform.position.z+CardIndex*0.01f;



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

            CardsInDiscard = new List<GameObject>();
        }

        if (CardsInDeck.Count != 0)
        {
            CardsInHand.Add(CardsInDeck[0]);

            CardsInDeck[0].transform.position = Deck.transform.position;
           
            CardsInDeck[0].GetComponent<JL_CardController>().Position = GetCardHandPosition(CardsInDeck[0]);

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


}
