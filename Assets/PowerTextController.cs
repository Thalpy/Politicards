using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTextController : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI powerText;
    // Start is called before the first frame update
    [SerializeField] string targetFactionName;

    [SerializeField] string factionSpecificText;
    void Start()
    {
        powerText = GetComponent<TMPro.TextMeshProUGUI>();
        GameMaster.factionController.factionPowerChanged.AddListener(onUpdateFactionPower);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onUpdateFactionPower()
    {
        Faction faction = GameMaster.factionController.SelectFaction(targetFactionName);
        powerText.text = factionSpecificText + faction.FactionPower.ToString();
        //Debug.Log("Faction power changed event received on power text controller");
    }
}
