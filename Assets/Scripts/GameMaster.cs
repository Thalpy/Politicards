using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static CrisisMaster crisisMaster;
    
    // Start is called before the first frame update
    void Start()
    {
         crisisMaster = GetComponent<CrisisMaster>();
    }
}