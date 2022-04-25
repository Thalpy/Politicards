using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieChartTextBoxController : MonoBehaviour
{
    [SerializeField] string FactionName;

    [SerializeField] float factionPower;

    [SerializeField] float factionHappiness;

    [SerializeField] TMPro.TextMeshProUGUI FactionNameBox;

    [SerializeField] TMPro.TextMeshProUGUI FactionPowerBox;

    [SerializeField] TMPro.TextMeshProUGUI FactionHappinessBox;


    [SerializeField] GameObject widget;

    [SerializeField] UIhandler uIhandler;

    GameObject gameMaster;


    void Start()
    {
        FactionNameBox = widget.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
        FactionPowerBox = widget.transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<TMPro.TextMeshProUGUI>();
        FactionHappinessBox = widget.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<TMPro.TextMeshProUGUI>();
        gameMaster = uIhandler.gameMaster;
        uIhandler.PieChartClicked.AddListener(onPieChartClicked);
        uIhandler.PieChartMouseExit.AddListener(onPieChartLeft); 
        FactionNameBox.SetText(FactionName);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPieChartClicked(string factionName)
    {
        Debug.Log("Pie chart clicked event received on pie chart text box controller");
        widget.SetActive(true);
        Faction faction = GetFaction(factionName);
        FactionNameBox.text = faction.FactionName;
        FactionPowerBox.text = faction.FactionPower.ToString();
        FactionHappinessBox.text = faction.FactionHappiness.ToString();
    }

    public Faction GetFaction(string factionName)
    {
        return gameMaster.GetComponent<FactionController>().SelectFaction(factionName);
    }

    public void onPieChartLeft()
    {
        Debug.Log("Pie chart mouse exit event received on pie chart text box controller");
        widget.SetActive(false);
    }
}
