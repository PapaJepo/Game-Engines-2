using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    private float time = 120f;
    public List<GameObject> Cameras;

    public List<GameObject> ShipSpawns;

    public GameObject ShipDock;
    public GameObject DockPath;

    public GameObject Cruiser;

    public GameObject FadeOut;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if( time < 110f && time > 100f)
        {
            Cameras[0].SetActive(false);
            Cameras[1].SetActive(true);
        }
        else if(time < 100f && time > 90f)
        {
            Cameras[1].SetActive(false);
            Cameras[2].SetActive(true);
        }
        else if (time < 90f && time > 70f)
        {
            Cameras[2].SetActive(false);
            Cameras[3].SetActive(true);
        }
        else if (time < 70f && time > 50f)
        {
            Cameras[3].SetActive(false);
            Cameras[4].SetActive(true);
            ShipSpawns[0].SetActive(true);
            ShipSpawns[1].SetActive(true);
        }
        else if (time < 50f && time > 30f)
        {
            Cameras[4].SetActive(false);
            Cameras[0].SetActive(true);
            Cruiser.GetComponent<Boid>().enabled = true;
            Cruiser.GetComponent<Arrive>().enabled = true;
        }
        else if (time < 20f && time > 10f)
        {
            Cameras[0].SetActive(false);
            Cameras[1].SetActive(true);
            ShipDock.GetComponent<FollowPath>().path = DockPath.GetComponent<Path>();
        }
        else if (time < 10f)
        {
            FadeOut.SetActive(true);
            
        }



    }
}
