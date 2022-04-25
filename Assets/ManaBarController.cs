using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarController : MonoBehaviour
{
    [SerializeField] GameObject GameMaster;

    [SerializeField] string FactionName;

    FactionController factionController;

    [SerializeField] Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        factionController = GameMaster.GetComponent<FactionController>();
        factionController.ManaEvent.AddListener(onManaChange);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onManaChange(float mana, string factionName)
    {
        Debug.Log("Mana changed event received on mana bar controller");
       // check that the name passed in matches the name of the faction and if it does update the mana bar
         if (factionName == FactionName)
         {
              image.fillAmount = mana;
         }
    }
}
