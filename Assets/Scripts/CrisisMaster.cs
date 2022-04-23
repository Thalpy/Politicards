using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to store the list of possible crisises.
/// It's supposed to be added to in the unity editor.
/// </summary>
public class CrisisMaster : MonoBehaviour
{
    [SerializeField]
    public List<Crisis> crisises = new List<Crisis>();
    ActiveCrisis[] activeCrisses = new ActiveCrisis[3];
    //TODO:
    // Track cards applied to events

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class ActiveCrisis
{
    public Crisis crisis;
    public Timer timer;
    public Card[] playerCards = new Card[3];
    public Card[] AICards = new Card[3];

    public ActiveCrisis(Crisis _crisis)
    {
        crisis = _crisis.Copy();
        timer = new Timer(crisis.DayLength, crisis.StartCrisis);
    }
}
