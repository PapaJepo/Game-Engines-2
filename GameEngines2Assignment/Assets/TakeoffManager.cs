using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeoffManager : MonoBehaviour
{
    public Steering ShipRef;
    public List<GameObject> cameras;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ShipRef.CurrentWaypoint == 1)
        {
            cameras[0].SetActive(false);
            cameras[1].SetActive(true);
        }
    }
}
