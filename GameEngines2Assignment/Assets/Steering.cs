using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public float Mass = 15;
    public float MaxVelocity = 3;
    public float MaxForce = 15;

    private Vector3 velocity;

    public List<Transform> target;
    private int CurrentWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        CurrentWaypoint = 0;
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWyapoints();
        Seek();

    }


    void UpdateWyapoints()
    {
        if (Vector3.Distance(target[CurrentWaypoint].position, this.transform.position) < 5)
        {
            CurrentWaypoint = (CurrentWaypoint + 1) % target.Count;
          
        }
    }

    void Seek()
    {
        var desiredVelocity = target[CurrentWaypoint].position - transform.position;
        desiredVelocity = desiredVelocity.normalized * MaxVelocity;

        var steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, MaxForce);
        steering /= Mass;

        velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);
        transform.position += velocity * Time.deltaTime;
        transform.forward = velocity.normalized;

        Debug.DrawRay(transform.position, velocity.normalized * 2, Color.green);
        Debug.DrawRay(transform.position, desiredVelocity.normalized * 2, Color.magenta);
    }
}
