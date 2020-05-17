using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    public GameObject EnemyShip;
    public LayerMask EnemyLayer;
    public Collider[] Detection;

    private float time;
    public float FireRate;
    public GameObject Bullet;

    // Start is called before the first frame update
    void Start()
    {
        time = FireRate;
    }

    // Update is called once per frame
    void Update()
    {
        Detection = Physics.OverlapSphere(this.transform.position + transform.forward * 300f, 100f, EnemyLayer);
        if (Detection.Length > 0)
        {
            Shooting();
        }
    }

    void Shooting()
    {
        if(time <= 0f)
        {
            GameObject tempBullet = Instantiate(Bullet, this.transform.position, Quaternion.identity);
            tempBullet.GetComponent<Rigidbody>().velocity = (EnemyShip.transform.position - this.transform.position).normalized * 300f;
            time = FireRate;
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position + transform.forward * 200f, 50f);
    }
}
