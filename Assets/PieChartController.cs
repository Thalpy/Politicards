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

    public GameObject PieSector5;




    // Start is called before the first frame update
    void Awake()
    {
        PieSector1.GetComponent<PieChartMesh>().startAngle = 0;
        PieSector1.GetComponent<PieChartMesh>().Proportion = 0.2;
        Instantiate(PieSector1, GetComponent<Transform>().position + new Vector3(0, 0, -0.25f), Quaternion.identity);
        PieSector2.GetComponent<PieChartMesh>().startAngle = 72;
        PieSector2.GetComponent<PieChartMesh>().Proportion = 0.2;
        Instantiate(PieSector2, GetComponent<Transform>().position + new Vector3(0, 0, -0.25f), Quaternion.identity);
        PieSector3.GetComponent<PieChartMesh>().startAngle = 144;
        PieSector3.GetComponent<PieChartMesh>().Proportion = 0.2;
        Instantiate(PieSector3, GetComponent<Transform>().position + new Vector3(0, 0, -0.25f), Quaternion.identity);
        PieSector4.GetComponent<PieChartMesh>().startAngle = 218;
        PieSector4.GetComponent<PieChartMesh>().Proportion = 0.2;
        Instantiate(PieSector4, GetComponent<Transform>().position + new Vector3(0, 0, -0.25f), Quaternion.identity);
        PieSector5.GetComponent<PieChartMesh>().startAngle = 290;
        PieSector5.GetComponent<PieChartMesh>().Proportion = 0.2;
        Instantiate(PieSector5, GetComponent<Transform>().position + new Vector3(0, 0, -0.25f), Quaternion.identity);

        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
