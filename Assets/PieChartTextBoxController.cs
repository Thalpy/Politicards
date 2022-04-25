using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieChartTextBoxController : MonoBehaviour
{
    [SerializeField] string FactionName;

    [SerializeField] float factionPower;

    [SerializeField] float factionHappiness;

    [SerializeField] TMPro.TextMeshProUGUI FactionNameText;

    [SerializeField] TMPro.TextMeshProUGUI FactionPowerText;

    [SerializeField] TMPro.TextMeshProUGUI FactionManaText;

    [SerializeField] TMPro.TextMeshProUGUI FactionHappinessText;


    [SerializeField] GameObject widget;

    [SerializeField] UIhandler uIhandler;

    GameObject gameMaster;


    void Start()
    {
        gameMaster = uIhandler.gameMaster;
        uIhandler.PieChartClicked.AddListener(onPieChartClicked);
        uIhandler.PieChartMouseExit.AddListener(onPieChartLeft);
        widget.SetActive(false);

    }

    /// <summary>
    /// called when the pie chart is clicked
    /// given the faction name, sets the text boxes to the correct values
    /// </summary>
    public void onPieChartClicked(string factionName)
    {
        Debug.Log("Pie chart clicked event received on pie chart text box controller");
        widget.SetActive(true);
        Faction faction = GetFaction(factionName);
        FactionNameText.text = faction.FactionName;
        FactionPowerText.text = faction.FactionPower.ToString();
        FactionManaText.text = faction.PlayerMana.ToString();
        FactionHappinessText.text = faction.FactionHappiness.ToString();
    }

    /// <summary>
    /// Given the faction name, returns the faction object
    /// </summary>
    public Faction GetFaction(string factionName)
    {
        return gameMaster.GetComponent<FactionController>().SelectFaction(factionName);
    }


    /// <summary>
    /// called when the pie chart is left
    /// hides the widget
    /// </summary>
    public void onPieChartLeft()
    {
        Debug.Log("Pie chart mouse exit event received on pie chart text box controller");
        widget.SetActive(false);
    }
}
