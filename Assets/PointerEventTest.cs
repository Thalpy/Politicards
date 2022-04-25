using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerEventTest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] Vector2 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse entered");

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exited");
    }


}
