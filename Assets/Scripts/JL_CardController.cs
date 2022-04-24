using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JL_CardController : MonoBehaviour
{
    //Feeling variable might comment later
    // Gameobject and component references
    public GameObject Hand;
    public GameObject Deck;
    public GameObject Discard;
    
    JL_HandController _HandController;
    
    public Vector3 Position; //public but y?

    //State Falgs ?????
    public bool Grabbed;
    public bool Discarded;
    public bool InHand;
    public bool InAdversariesHand;
    public bool Zoomed;
    public bool AdversaryZoomed;

    public Card _Card;

  
    // All the magic numebers?
    [SerializeField] float MinCardVelocity;

    [SerializeField] float CardVeloscityDistanceFactor;

    
    [SerializeField] float ZoomScale;

    [SerializeField] float ZoomSpeed;

    [SerializeField] float TargetTriggerDistance;

    [SerializeField] Vector3 AdversaryZoomedOffset;

    [SerializeField] GameObject CardTitle;

    [SerializeField] GameObject CardDescription;

    [SerializeField] GameObject CardPicture;

   
    
    void Start() //initalises some component referemces
    {
    _HandController = Hand.GetComponent<JL_HandController>();
    gameObject.name = _Card.Name;
    CardTitle.GetComponent<TMPro.TextMeshPro>().text = _Card.Name;
    CardDescription.GetComponent<TMPro.TextMeshPro>().text = _Card.Description;
    CardPicture.GetComponent<SpriteRenderer>().sprite = _Card.image;
    }

    // Update is called once per frame
    void Update() //lots of things happen hear, UI stuff so in Update.
    {
    

    if (InHand) //functions if in player hand.
    {
    if (Input.GetMouseButtonDown(0)) //detects mouse button down and checks if it was clicking a card, if it is it grabs the card casing it to follow the mouse
    {
        Ray _Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _Hit;

        if (Physics.Raycast(_Ray, out _Hit, 200))
        {
            
            if (_Hit.transform == transform && !Discarded)
                {
                Grabbed = true;
                }

        }
    }
    
    if (Grabbed == true) // checks if it's grabbed if so follows the mouse
    {
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;
        float z = transform.position.z - Camera.main.transform.position.z;
        Position = Camera.main.ScreenToWorldPoint(new Vector3 (x,y,z));
        SetDrawOrder(1000);

        if (Input.GetKeyDown("space")) // on space zooms the card
        {
            Zoomed = !Zoomed;
        }

    }

    if (Grabbed && Zoomed) // zooms the card if it is grabbed and space has been pressed
    {
        if (transform.localScale.x < ZoomScale)
        {
            transform.localScale += new Vector3 (1,1,1)*Time.deltaTime*ZoomSpeed;
            if (transform.localScale.x > ZoomScale)
            {
                transform.localScale = new Vector3 (ZoomScale,ZoomScale,ZoomScale);
            }
        }
    }
    else if (transform.localScale.x > 1) // if not zoomed checks if it's not the default size and ifnot shrinks it back down.
    {
        transform.localScale -= new Vector3 (1,1,1)*Time.deltaTime*ZoomSpeed;
        if (transform.localScale.x < 1)
        {
            transform.localScale = new Vector3 (1,1,1);
        }
    }
    

    if (Input.GetMouseButtonUp(0)&&Grabbed) // on mouse up ungrabs the card, also checks if it's near a "target" and will play it ++NEEDS FINISHING ONCE TARGETS ARE IMPLEMENTED++
    {
        Grabbed = false;
        bool AtTarget = false;
        float TargetDistance = 1000000000; //so large maybe too large? //What is this????
        GameObject Target;

        for (int i = 0; i < GameMaster.Targets.Count; i++) //loops through targerts and checks if they are near the cards position, gets the nearest target to the center of the card.
        {
            float NewTargetDistance = Vector2.Distance(gameObject.transform.position, GameMaster.Targets[i].transform.position);
            Target = GameMaster.Targets[i];
            Targetable targetObj = Target.GetComponent<Targetable>();
            if (Target.GetComponent<Targetable>() == null)
            {
                Debug.LogWarning("Targetable component not found on target");
                Debug.Break();
            }
            //Is this ok landy?
            if (NewTargetDistance < targetObj.allowedDistance)
            {
                TargetDistance = NewTargetDistance;
                AtTarget = true;
                Target.GetComponent<Targetable>().DropCard(_Card);
            }
            

            // if (NewTargetDistance < TargetDistance && NewTargetDistance < TargetTriggerDistance)
            // {
                
            //     TargetDistance = NewTargetDistance;
            //     //check to see if the target has targetable components
            //     if (Target.GetComponent<Targetable>() != null)
            //     {
                    
            //     }
            // }
        }

        if (AtTarget)
        {
            //Add on play effect hook here;
        }
        else
        {
            int DrawOrder;
            Position = _HandController.GetCardHandPosition(gameObject, out DrawOrder); // if no target then goesback to hand on mouse up
            SetDrawOrder(DrawOrder);

        }
        
    }
    }


    else if (InAdversariesHand) //in adversaries hands effects
    {
        Ray _Ray = Camera.main.ScreenPointToRay(Input.mousePosition); //checks if the mouse is over the object
        RaycastHit _Hit;

        if (Physics.Raycast(_Ray, out _Hit, 200))
        {
            if (_Hit.transform == transform && !Discarded)
            {
                int DrawOrder;
                Position = _HandController.GetCardHandPosition(gameObject, out DrawOrder) + AdversaryZoomedOffset;
                SetDrawOrder(1000);
                AdversaryZoomed = true; // and zooms
            }
            
            
        }
        else //if not mouse over then unzooms
        {
            AdversaryZoomed = false; 
        }

        if (AdversaryZoomed) //if zoomed it scales up/down over a few frames this happens over the course of a few frames
        {
            if (transform.localScale.x < ZoomScale*2)
            {
                transform.localScale += new Vector3 (1,1,1)*Time.deltaTime*ZoomSpeed*2;
                if (transform.localScale.x > ZoomScale*2)
                {
                    transform.localScale = new Vector3 (ZoomScale*2,ZoomScale*2,ZoomScale*2);
                }
            }
        }
            else if (transform.localScale.x > 1)
            {
                transform.localScale -= new Vector3 (1,1,1)*Time.deltaTime*ZoomSpeed*2;
                if (transform.localScale.x < 1)
                {
                    transform.localScale = new Vector3 (1,1,1);
                }
            }
        }



    if (Vector3.Distance(Discard.transform.position,transform.position)<0.1 && Discarded) //detects if near to the dicard pile after being dicarded and moves the card to the discard pile
    {
        transform.position =  _HandController.DiscardOffScreenLocation;
        Position = _HandController.DiscardOffScreenLocation;
        Discarded = false;
    }

    if(!Grabbed && !Discarded && !AdversaryZoomed && (InHand||InAdversariesHand)) //if nothing else the position in the hand is updated (in case cards have been drawn/played)
    {
        int DrawOrder;
        Position = _HandController.GetCardHandPosition(gameObject, out DrawOrder);
        SetDrawOrder(DrawOrder);
    }
            

            
            
    transform.position = Vector3.MoveTowards(transform.position, Position, Time.deltaTime*(GetVelocity())); // finally this is the line that actually moves the card to the position
    }

    float GetVelocity() //funtion to get the velocity
    {
    
    float VelocityOut;
    float VelocityMult;

    if(Grabbed){VelocityMult = 5;}else{VelocityMult=1;}


    VelocityOut = (MinCardVelocity+ CardVeloscityDistanceFactor*Vector3.Distance(Position,transform.position))*VelocityMult;

    return(VelocityOut);
    }

    public void DiscardAction() //function to call to discard a card, updates the various flags appropiately
    {
        Discarded = true;
        InHand = false;
        InAdversariesHand = false;
        _HandController.CardsInHand.Remove(gameObject);
        _HandController.CardsInDiscard.Add(gameObject);
       transform.SetParent(Discard.transform);
       Position = Discard.transform.position;
    }

    public void PlayAction()
    {

    }
    public void SetDrawOrder(int DrawOrder)
    {
    GetComponent<SpriteRenderer>().sortingOrder = DrawOrder;
    CardPicture.GetComponent<SpriteRenderer>().sortingOrder = DrawOrder - 1;
    CardTitle.GetComponent<TMPro.TextMeshPro>().sortingOrder = DrawOrder + 1;
    CardDescription.GetComponent<TMPro.TextMeshPro>().sortingOrder = DrawOrder + 1;
    
    
      
    }
}
