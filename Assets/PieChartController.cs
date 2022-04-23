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

    void updatePieChart()
    {
        EconomyPieSector.GetComponent<PieChartMesh>().Proportion = Economic.powerProportion;
        PeoplePieSector.GetComponent<PieChartMesh>().Proportion = People.powerProportion;
        NobilityPieSector.GetComponent<PieChartMesh>().Proportion = Nobility.powerProportion;
        MilitaryPieSector.GetComponent<PieChartMesh>().Proportion = Military.powerProportion;
        CrimePieSector.GetComponent<PieChartMesh>().Proportion = crime.powerProportion;

        //dispose of all of the PieChartSectors
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("PieChartSector"))
        {
            Debug.Log("Disposing of " + go.name);
            Destroy(go);
        }

        //set the start angle of each PieChartSector
        EconomyPieSector.GetComponent<PieChartMesh>().startAngle = 0;
        PeoplePieSector.GetComponent<PieChartMesh>().startAngle = (int) (Economic.powerProportion * 360);
        NobilityPieSector.GetComponent<PieChartMesh>().startAngle = (int) (Economic.powerProportion * 360 + People.powerProportion * 360);
        MilitaryPieSector.GetComponent<PieChartMesh>().startAngle = (int) (Economic.powerProportion * 360 + People.powerProportion * 360 + Nobility.powerProportion * 360);
        CrimePieSector.GetComponent<PieChartMesh>().startAngle = (int) (Economic.powerProportion * 360 + People.powerProportion * 360 + Nobility.powerProportion * 360 + Military.powerProportion * 360);

        //re-instantiate the PieChartSectors
        Instantiate(EconomyPieSector, GetComponent<Transform>().position + new Vector3(0, 0, -0.3f), Quaternion.identity);
        Instantiate(PeoplePieSector, GetComponent<Transform>().position + new Vector3(0, 0, -0.3f), Quaternion.identity);
        Instantiate(NobilityPieSector, GetComponent<Transform>().position + new Vector3(0, 0, -0.3f), Quaternion.identity);
        Instantiate(MilitaryPieSector, GetComponent<Transform>().position + new Vector3(0, 0, -0.3f), Quaternion.identity);
        Instantiate(CrimePieSector, GetComponent<Transform>().position + new Vector3(0, 0, -0.3f), Quaternion.identity);
         
        }
    public void updatePartyPower(string partyName, float power)
    {
        //find the party with the name partyName
        PoliticalParty party = PartyList.Find(x => x.partyName == partyName);
        //set the power proportion of the party to power
        party.powerProportion = power;
        //update the pie chart
        updatePieChart();


    }

    //public function to update a random party's power proportion by a random amount
    public void updateRandomPartyPower()
    {
        //get a random party
        PoliticalParty party = PartyList[Random.Range(0, PartyList.Count)];
        //get a random amount to add to the party's power proportion
        float amount = Random.Range(-0.1f, 0.1f);
        //add the amount to the party's power proportion
        updatePartyPower(party.partyName, party.powerProportion + amount);
    }


}
