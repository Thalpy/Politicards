using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionController : MonoBehaviour
{
    // Start is called before the first frame update

    // a serializable public array of all the factions

     [SerializeField] Faction[] factions;

    void Start()
    {
        // initialize factions with 5 Faction objects
        factions = GetComponent<FactionController>().factions;


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
