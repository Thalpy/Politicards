using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PoliticalParty
{
    public string partyName;
    public float powerProportion;
}




public class PieChartController : MonoBehaviour
{

    public PoliticalParty PartyA;
    public PoliticalParty PartyB;
    public PoliticalParty PartyC;
    public PoliticalParty PartyD;
    public GameObject PieSector1;
    public GameObject PieSector2;
    public GameObject PieSector3;
    public GameObject PieSector4;



    // Start is called before the first frame update
    void Awake()
    {
        PieSector1.GetComponent<PieChartMesh>().startAngle = 0;
        PieSector2.GetComponent<PieChartMesh>().startAngle = 90;
        PieSector3.GetComponent<PieChartMesh>().startAngle = 180;
        PieSector4.GetComponent<PieChartMesh>().startAngle = 270;
        Instantiate(PieSector1, GetComponent<Transform>().position + new Vector3(0, 0, -0.25f), Quaternion.identity);
        Instantiate(PieSector2, GetComponent<Transform>().position + new Vector3(0, 0, -0.25f), Quaternion.identity);
        Instantiate(PieSector3, GetComponent<Transform>().position + new Vector3(0, 0, -0.25f), Quaternion.identity);
        Instantiate(PieSector4, GetComponent<Transform>().position + new Vector3(0, 0, -0.25f), Quaternion.identity);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
