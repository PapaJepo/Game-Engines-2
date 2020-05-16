using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        
        switch(ShipRef.CurrentWaypoint)
        {
            case 1:
                cameras[0].SetActive(false);
                cameras[1].SetActive(true);
                break;
            case 2:
                cameras[1].SetActive(false);
                cameras[2].SetActive(true);
                break;
            case 3:
                cameras[2].SetActive(false);
                cameras[3].SetActive(true);
                break;
            case 4:
                cameras[3].SetActive(false);
                cameras[4].SetActive(true);
                break;
            case 5:
                SceneManager.LoadScene(1);
                break;


        }
    }
}
