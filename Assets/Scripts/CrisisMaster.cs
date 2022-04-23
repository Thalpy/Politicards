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
    List<Crisis> crises = new List<Crisis>();
    Crisis[] activeCrisses = new Crisis[3];
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
