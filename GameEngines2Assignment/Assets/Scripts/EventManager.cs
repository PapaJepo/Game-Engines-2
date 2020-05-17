using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    private float time = 120f;
    public List<GameObject> Cameras;
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
    }
}
