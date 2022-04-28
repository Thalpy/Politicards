using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JL_EventMover : MonoBehaviour
{
    [SerializeField] GameObject Table;

    [SerializeField] GameObject ActivePosition;

    [SerializeField] List<GameObject> Events;
    [SerializeField] List<Vector3> EventLocations;

    [SerializeField] List<Vector3> EventScales;

    [SerializeField] List<Quaternion> EventRotations;
    [SerializeField] List<bool> Active;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void AddEvent(GameObject Crisis)
    {
        Events.Add(Crisis);
        EventLocations.Add(Crisis.transform.position);
        EventScales.Add(Crisis.transform.localScale);
        EventRotations.Add(Crisis.transform.rotation);
        Active.Add(false);
    }

    public void RemoveEvent(GameObject Crisis)
    {
        for (int i = 0; i < Events.Count; i++)
        {
            if (Events[i] == Crisis)
            {
                Events.RemoveAt(i);
                EventLocations.RemoveAt(i);
                EventScales.RemoveAt(i);
                EventRotations.RemoveAt(i);
                Active.RemoveAt(i);

            }
        }
    }

    void Update()
    {
        for (int i = 0; i < Events.Count; i++)
        {
            if(!Active[i] && Events[i].transform.position != EventLocations[i])
            {
                float Distance = Vector3.Distance(EventLocations[i],Events[i].transform.position);
                float TotalDistance = Vector3.Distance(EventLocations[i],ActivePosition.transform.position);
                
                float k = Distance/TotalDistance;
                k = k - Time.deltaTime;
                //Debug.Log(k);
                if (k<0) {k=0;}

                Events[i].transform.position = Vector3.Lerp(EventLocations[i],ActivePosition.transform.position,k);
                Events[i].transform.localScale = Vector3.Lerp(EventScales[i],ActivePosition.transform.localScale,k);
                Events[i].transform.rotation = Quaternion.Lerp(EventRotations[i],ActivePosition.transform.rotation,k);
            }
            else if(Active[i] && Events[i].transform.position != ActivePosition.transform.position)
            {
                float Distance = Vector3.Distance(EventLocations[i],Events[i].transform.position);
                float TotalDistance = Vector3.Distance(EventLocations[i],ActivePosition.transform.position);
                
                float k = Distance/TotalDistance;
                k = k + Time.deltaTime;
                Debug.Log(k);
                if (k>1) {k=1;}

                Events[i].transform.position = Vector3.Lerp(EventLocations[i],ActivePosition.transform.position,k);
                Events[i].transform.localScale = Vector3.Lerp(EventScales[i],ActivePosition.transform.localScale,k);
                Events[i].transform.rotation = Quaternion.Lerp(EventRotations[i],ActivePosition.transform.rotation,k);
            }
        }
    }

    public bool IsActive(GameObject Crisis)
    {
        for (int i = 0; i < Events.Count; i++)
        {
            if (Events[i] == Crisis)
            {
                return Active[i];
            }
        }
        return false;
    }

    public void SetActive(GameObject Crisis)
    {
        
    
        for (int i = 0; i < Events.Count; i++)
        {
            if (Events[i] == Crisis)
            {

                Active[i] = true;

            }
        }
    

    }

    public void SetInactive(GameObject Crisis)
    {
        
    
        for (int i = 0; i < Events.Count; i++)
        {
            if (Events[i] == Crisis)
            {

                Active[i] = false;

            }
        }
    }

    public void InactiveAll()
    {
        for (int i = 0; i < Events.Count; i++)
        {
            Active[i] = false;
        }
    }

    public void SetSingleActive(GameObject crisis)
    {
        for (int i = 0; i < Events.Count; i++)
        {
            if (Events[i] == crisis)
            {
                Active[i] = true;
            }
            else
            {
                Active[i] = false;
            }
        }
    }
}
