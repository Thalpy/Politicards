using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PoliticalParty
{
    public string partyName;
    public float powerProportion;

    public Material mat;

    public PoliticalParty(string partyName, float powerProportion, Material mat)
    {
        this.partyName = partyName;
        this.powerProportion = powerProportion;
        this.mat = mat;
    }

}




public class PieChartController : MonoBehaviour
{

    public PoliticalParty People;
    public PoliticalParty Economic;
    public PoliticalParty Military;
    public PoliticalParty Nobility;

    public PoliticalParty crime;
    public GameObject EconomyPieSector;
    public GameObject PeoplePieSector;
    public GameObject NobilityPieSector;
    public GameObject MilitaryPieSector;

    public GameObject CrimePieSector;

    public List<PoliticalParty> PartyList;

    //create a hash table to store the party name and the PieChartSector
    public Dictionary<string, GameObject> PartyDictionary;





    // Start is called before the first frame update
    void Awake()
    {

        //Instantiate People with partyName = "People", powerProportion = 0.2f, mat = People
        People = new PoliticalParty("People", 0.2f, Resources.Load("Materials/People", typeof(Material)) as Material);

        //Instantiate Economic with partyName = "Economic", powerProportion = 0.2f, mat = Economic
        Economic = new PoliticalParty("Economic", 0.2f, Resources.Load("Materials/Economic", typeof(Material)) as Material);

        //Instantiate Military with partyName = "Military", powerProportion = 0.2f, mat = Military
        Military = new PoliticalParty("Military", 0.2f, Resources.Load("Materials/Military", typeof(Material)) as Material);

        //Instantiate Nobility with partyName = "Nobility", powerProportion = 0.2f, mat = Nobility
        Nobility = new PoliticalParty("Nobility", 0.2f, Resources.Load("Materials/Nobility", typeof(Material)) as Material);

        crime = new PoliticalParty("Crime", 0.2f, Resources.Load("Materials/Crime", typeof(Material)) as Material);

        //add all of the parties to the PartyList
        PartyList = new List<PoliticalParty>();
        PartyList.Add(People);
        PartyList.Add(Economic);
        PartyList.Add(Military);
        PartyList.Add(Nobility);
        PartyList.Add(crime);

        SetupPieChart();
    }

    void SetupPieChart()
    {
        EconomyPieSector.GetComponent<PieChartMesh>().startAngle = 0;
        EconomyPieSector.GetComponent<PieChartMesh>().Proportion = Economic.powerProportion;
        PeoplePieSector.GetComponent<PieChartMesh>().startAngle = 72;
        PeoplePieSector.GetComponent<PieChartMesh>().Proportion = People.powerProportion;
        NobilityPieSector.GetComponent<PieChartMesh>().startAngle = 144;
        NobilityPieSector.GetComponent<PieChartMesh>().Proportion = Nobility.powerProportion;
        MilitaryPieSector.GetComponent<PieChartMesh>().startAngle = 216;
        MilitaryPieSector.GetComponent<PieChartMesh>().Proportion = Military.powerProportion;
        CrimePieSector.GetComponent<PieChartMesh>().startAngle = 288;
        CrimePieSector.GetComponent<PieChartMesh>().Proportion = crime.powerProportion;

        Instantiate(EconomyPieSector, GetComponent<Transform>().position + new Vector3(0, 0, -0.3f), Quaternion.identity);
        Instantiate(PeoplePieSector, GetComponent<Transform>().position + new Vector3(0, 0, -0.3f), Quaternion.identity);
        Instantiate(NobilityPieSector, GetComponent<Transform>().position + new Vector3(0, 0, -0.3f), Quaternion.identity);
        Instantiate(MilitaryPieSector, GetComponent<Transform>().position + new Vector3(0, 0, -0.3f), Quaternion.identity);
        Instantiate(CrimePieSector, GetComponent<Transform>().position + new Vector3(0, 0, -0.3f), Quaternion.identity);

    }



/*

        
        PieSector1.GetComponent<PieChartMesh>().startAngle = 0;
        PieSector1.GetComponent<PieChartMesh>().Proportion = 0.2;
        //set the material of PieSector1 to the People material
        Instantiate(PieSector1, GetComponent<Transform>().position + new Vector3(0, 0, -0.25f), Quaternion.identity);
        PieSector1.GetComponent<MeshRenderer>().material = Resources.Load("Materials/People", typeof(Material)) as Material;
        PieSector2.GetComponent<PieChartMesh>().startAngle = 72;
        PieSector2.GetComponent<PieChartMesh>().Proportion = 0.2;
        // set the material of PieSector2 to the Economy material
        PieSector2.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Economic", typeof(Material)) as Material;
        Instantiate(PieSector2, GetComponent<Transform>().position + new Vector3(0, 0, -0.25f), Quaternion.identity);
        PieSector3.GetComponent<PieChartMesh>().startAngle = 144;
        PieSector3.GetComponent<PieChartMesh>().Proportion = 0.2;
        // set the material of PieSector3 to the Military material
        PieSector3.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Military", typeof(Material)) as Material;
        Instantiate(PieSector3, GetComponent<Transform>().position + new Vector3(0, 0, -0.25f), Quaternion.identity);
        PieSector4.GetComponent<PieChartMesh>().startAngle = 218;
        PieSector4.GetComponent<PieChartMesh>().Proportion = 0.2;
        //set the material of PieSector4 to the Nobility material
        PieSector4.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Nobility", typeof(Material)) as Material;
        Instantiate(PieSector4, GetComponent<Transform>().position + new Vector3(0, 0, -0.25f), Quaternion.identity);
        PieSector5.GetComponent<PieChartMesh>().startAngle = 290;
        PieSector5.GetComponent<PieChartMesh>().Proportion = 0.2;
        //set the material of PieSector5 to the Crime material
        PieSector5.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Crime", typeof(Material)) as Material;
        Instantiate(PieSector5, GetComponent<Transform>().position + new Vector3(0, 0, -0.25f), Quaternion.identity);

*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
