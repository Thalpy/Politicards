using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JL_EventMoverTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameMaster._JL_EventMover.AddEvent(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
