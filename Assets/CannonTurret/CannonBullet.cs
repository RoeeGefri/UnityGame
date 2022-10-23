using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    public Transform target = null;
    public Vector3 targetPosition;
    private Rigidbody rb;
    public GameObject explosion;

    private Transform ballons;
    private float bulletSpeed = 15f;
    private float bulletRange = 5f;
    private int damage = 100;

    // Start is called before the first frame updated
    void Start()
    {
        if (target == null) return;

        targetPosition = target.position;

        //predict
        Vector3 nextWayPoint = target.GetComponent<BallonScript>().target;
        float distance = Vector3.Distance(nextWayPoint, targetPosition);
        if (distance >= 11)
        {
            targetPosition += (nextWayPoint - targetPosition).normalized*11;
        }
        else
        {
            targetPosition = nextWayPoint;
            targetPosition += (target.GetComponent<BallonScript>().GetNextPoint(nextWayPoint) - targetPosition).normalized*(11-distance);
        }
        


      
        rb = GetComponent<Rigidbody>();
        rb.AddForce(0,Vector3.Distance(targetPosition,transform.position)*45, 0);
        
    }



    // Update is called once per frame
    void Update()
    {
        transform.Translate((targetPosition - transform.position).normalized * bulletSpeed * Time.deltaTime, Space.World);
        
        if (Vector3.Distance(targetPosition, transform.position) < 0.25)
        {
            Boom(transform.position);
            Destroy(gameObject);
        }
        if(transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    
    private void Boom(Vector3 position)
    {
        GameObject go = Instantiate(explosion, position, Quaternion.identity,transform.parent);
        explosion.GetComponent<ParticleSystem>().Play();
        Destroy(go, 3);
        for (int i = 0; i < ballons.childCount; i++)
        {
            if(Vector3.Distance(ballons.GetChild(i).position,targetPosition) < bulletRange)
            {
                ballons.GetChild(i).gameObject.GetComponent<BallonScript>().Damage(damage);
            }
        }
        //add predict system
    }

    public void SetTarget(Transform ballon)
    {
        target = ballon;
    }

    public void SetBallons(Transform ballonsTra)
    {
        ballons = ballonsTra;
    }

}


