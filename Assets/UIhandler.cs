using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIhandler : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject UI;

    [SerializeField] PieChart pieChart;

    [SerializeField] public StringEvent PieChartClicked = new StringEvent();

    [SerializeField] public UnityEvent PieChartMouseExit = new UnityEvent();

    [SerializeField] internal GameObject gameMaster;


    // get reference to the event system
    EventSystem eventSystem;
    void Start()
    {
        eventSystem = EventSystem.current;
        

    }

    // Update is called once per frame
    void Update()
    {

    }

    // implement interfaces

    public void ProcessePieChartClick(GameObject pieChartSprite)
    {
        Debug.Log("I am being clicked");
        //get the current mouse position and print to the debug log
        Vector2 mousePosition = new Vector2();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetChild(5).GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out mousePosition);
        Debug.Log("Mouse position is: " + mousePosition);
        Debug.Log($"The pie chart is at {pieChartSprite.transform.position}");
        Vector2 relativeClickPosition = mousePosition - (Vector2)pieChartSprite.transform.position;
        Debug.Log($"The relative click position is {relativeClickPosition}");
        //get the distance between the pie chart and the mouse position
        float distance = Vector2.Distance(mousePosition, pieChartSprite.transform.position);
        float angle = NormalizeAngle(Mathf.Atan2(relativeClickPosition.y, relativeClickPosition.x) * Mathf.Rad2Deg);
        Debug.Log($"The angle is {angle}");
        Debug.Log($"The relative Progression is {angle / 360}");



        // find the PieChart in the scene

        Debug.Log($"The selected faction was {pieChart.GetPieChartData(angle/360)}");

        PieChartClicked.Invoke(pieChart.GetPieChartData(angle / 360));

    }

    // a function that takes an angle between -180 and 180 and returns the angle between 0 and 360
    public float NormalizeAngle(float angle)
    {
        if (angle < 0)
        {
            angle = 360 + angle;
        }
        return angle;
    }

    public void MouseLeftChart()
    {
        PieChartMouseExit.Invoke();
        Debug.Log("Mouse left the pie chart");
    }
    

    



}
