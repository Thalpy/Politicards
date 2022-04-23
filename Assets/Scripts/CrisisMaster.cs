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
    public CrisisBox crisisBox;
    //TODO:
    // Track cards applied to events

    // Start is called before the first frame update
    void Start()
    {
        //get the kirby crisis
        Crisis kirby = getCrisis("Kirby's Fooking Pissed");
        //Active the crisis
        ActivateCrisis(kirby);
    }

    public Crisis getCrisis(string name)
    {
        foreach (Crisis crisis in crisises)
        {
            if (crisis.Name == name)
            {
                return crisis;
            }
        }
        Debug.LogWarning("Crisis not found: " + name);
        return null;
    }

    //determines if a new crisis can be added
    public bool CanAddCrisis()
    {
        for (int i = 0; i < activeCrisses.Length; i++)
        {
            if (activeCrisses[i] == null)
            {
                return true;
            }
        }
        return false;
    }

    // activates a crisis
    public void ActivateCrisis(Crisis crisis)
    {
        if(!CanAddCrisis())
        {
            Debug.Log("CrisisMaster: All crises are active when we tried to activate a new one.");
            Debug.Break();
            return;
        }

        for (int i = 0; i < activeCrisses.Length; i++)
        {
            if (activeCrisses[i] == null)
            {
                activeCrisses[i] = new ActiveCrisis(crisis, crisisBox);
                return;
            }
        }
    }
}


public class ActiveCrisis
{
    public Crisis crisis;
    public Timer timer;
    public Card[] playerCards = new Card[3];
    public Card[] AICards = new Card[3];

    public ActiveCrisis(Crisis _crisis, CrisisBox crisisBox)
    {
        crisis = _crisis.Copy();
        timer = new Timer(crisis.DayLength, crisis.EndCrisis);
        crisis.StartCrisis();
        crisisBox.ChangeEvent(crisis);
    }


}
