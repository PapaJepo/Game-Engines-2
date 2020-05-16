using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public GameObject Banshee;

    public float Mass = 15;
    public float MaxVelocity = 6;
    public float MaxForce = 20;

    public Vector3 velocity;

    public List<Transform> target;
    private int CurrentWaypoint;

    public bool ship;

    Vector3 EnemyTarget;
    Vector3 offset;
    Vector3 WorldTarget;

    public Transform otherOffsetPos;

    // Start is called before the first frame update
    void Start()
    {
        CurrentWaypoint = 0;
        velocity = Vector3.zero;


    }

    // Update is called once per frame
    void Update()
    {

       
        if(ship == true)
        {
            OffsetPursue();
        }
        else if( ship == false)
        {
            UpdateWaypoints();
            Seek();

        }

    }


    void UpdateWaypoints()
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
    }

    void OffsetPursue()
    {
        offset = otherOffsetPos.position - Banshee.transform.position;

        WorldTarget = Banshee.transform.TransformPoint(offset);
        float dist = Vector3.Distance(WorldTarget, transform.position);
        float time = dist / MaxVelocity;
        EnemyTarget = WorldTarget + (Banshee.GetComponent<Steering>().velocity * time);

    }

    public Vector3 Arrive(Vector3 targetPos, float slowDown = 10f)
    {
        Vector3 targetDir = targetPos - transform.position;

        float distance = targetDir.magnitude;
        if(distance < 0.1f)
        {
            return Vector3.zero;
        }
        float ramped = MaxVelocity * (distance / slowDown);

        float clamped = Mathf.Min(ramped, MaxVelocity);
        Vector3 desiredVelocity = clamped * (targetDir / distance);
        return desiredVelocity - velocity;
    }
}
