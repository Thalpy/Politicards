using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchBigHandController : MonoBehaviour
{

    [SerializeField] UIhandler uIhandler;

    GameObject gameMaster;
    // Start is called before the first frame update
    void Start()
    {
        gameMaster = uIhandler.gameMaster;

        GameMaster.turnTimeController.turnTimeEvent.AddListener(OnTurnTime);



        
    }

    // Update is called once per frame
    public void OnTurnTime(int turn)
    {
        Debug.Log("Turn time event received on watch big hand controller");
        StartCoroutine(AnimateBigHand(turn));
    }

    IEnumerator AnimateBigHand(int turn)
    {
        // animate the big hand by rotating the transform of the current gameobject through 360 degrees
        for(int i = 0; i < 360; i++)
        {
            transform.Rotate(0, 0, -1);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
