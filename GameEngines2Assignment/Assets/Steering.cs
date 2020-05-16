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
    public Vector3 force;

    public List<Transform> target;
    public int CurrentWaypoint;

    public bool ship;
    public bool dock;

    Vector3 EnemyTarget;
    Vector3 offset;
    Vector3 WorldTarget;
    public Vector3 acceleration;

    public Transform otherOffsetPos;

    // Start is called before the first frame update
    void Start()
    {
        CurrentWaypoint = 0;
        velocity = Vector3.zero;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(dock == true)
        {
            velocity = velocity * 0;
            this.transform.rotation = this.transform.rotation;
        }
        /*Vector3 newAcceleration = force / Mass;
        acceleration = Vector3.Lerp(acceleration, newAcceleration, Time.deltaTime);
        velocity += acceleration * Time.deltaTime;

        velocity = Vector3.ClampMagnitude(velocity, MaxVelocity);

        if(velocity.magnitude > float.Epsilon)
        {
            transform.LookAt(transform.position + velocity, transform.up);

            transform.position += velocity * Time.deltaTime;

        }*/
       
        if(ship == true && dock == false)
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
        var desiredVelocity = Banshee.GetComponent<Steering>().otherOffsetPos.position - transform.position;
        desiredVelocity = desiredVelocity.normalized * MaxVelocity;

        var steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, MaxForce);
        steering /= Mass;

        velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);
        transform.position += velocity * Time.deltaTime;
        transform.forward = velocity.normalized;



    }
        /*Debug.Log("ewbfywbashjfbwq");
        offset = Banshee.GetComponent<Steering>().otherOffsetPos.position - Banshee.transform.position;
        //offset = Quaternion.Inverse(Banshee.transform.rotation) * offset;

        WorldTarget = Banshee.transform.TransformPoint(offset);
        float dist = Vector3.Distance(WorldTarget, transform.position);
        float time = dist / MaxVelocity;
        EnemyTarget = WorldTarget + (Banshee.GetComponent<Steering>().velocity * time);

        force = Arrive(EnemyTarget); 
        
        var desiredVelocity = Banshee.GetComponent<Steering>().otherOffsetPos.position - transform.position;
        desiredVelocity = desiredVelocity.normalized * MaxVelocity;

        var steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, MaxForce);
        steering /= Mass;

        velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);
        transform.position += velocity * Time.deltaTime;
        transform.forward = velocity.normalized;

    }*/

    public Vector3 Arrive()
    {
        Vector3 targetDir = Banshee.GetComponent<Steering>().otherOffsetPos.position - transform.position;

        float distance = targetDir.magnitude;
        if(distance < 0.1f)
        {
            return Vector3.zero;
        }
        float ramped = MaxVelocity * (distance / 10f);

        float clamped = Mathf.Min(ramped, MaxVelocity);
        Vector3 desiredVelocity = clamped * (targetDir / distance);
        return desiredVelocity - velocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + velocity);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(this.transform.position, this.transform.position + force);
    }
}
