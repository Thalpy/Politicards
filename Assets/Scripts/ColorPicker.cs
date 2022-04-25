using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    //COMPONENTS
    #region components

    [SerializeField] private GameMaster GameMaster;
    [SerializeField] RectTransform rectTransform;

    [SerializeField] BoxCollider boxCollider;

    [SerializeField] Vector3 boxColliderLocationfromTransformPoint;

    [SerializeField] Vector3 boxColliderLocationfromTransformTransformPoint;

    [SerializeField] Vector3 boxColliderLocationfromTransformPointInverse;

    [SerializeField] Vector3 boxColliderLocationfromTransformLocalPosition;

    [SerializeField] Camera camera;

    [SerializeField] Color colorUnderMouse;



    #endregion


    
    [SerializeField] Vector2 mousePosition;

    [SerializeField] bool isMouseOverPieChart;

    [SerializeField] Vector3 collisonPoint;


    void Start()
    {
          


    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = GetMousePosition();
        isMouseOverPieChart = IsMouseOverPieChart();
        boxColliderLocationfromTransformPoint = boxCollider.transform.position;
        boxColliderLocationfromTransformTransformPoint = transform.TransformPoint(boxCollider.transform.position);
        boxColliderLocationfromTransformPointInverse = transform.InverseTransformPoint(boxCollider.transform.position);
        boxColliderLocationfromTransformLocalPosition = boxCollider.transform.localPosition;
        
    
                    

    }


    //a function that gets the mouse position and returns it
    public Vector2 GetMousePosition()
    {
        return Input.mousePosition;
    }

    // a function that checks if the given screen position is over a UI element
    public bool IsMouseOverPieChart()
    {
        //create a new ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        //create a new raycast hit
        RaycastHit hit;
        //if the ray hits something
        if (Physics.Raycast(ray, out hit))
        {
            //if the raycast hit is a UI element
            if (hit.collider.tag == "PIE_CHART")
            {
                //return true
                collisonPoint = camera.WorldToScreenPoint(hit.point);
                return true;
                //update the collison point
                
            }
        }
        //if the raycast hit is not a UI element
        return false;
    }

    // capture the current screen to a texture


    




}
