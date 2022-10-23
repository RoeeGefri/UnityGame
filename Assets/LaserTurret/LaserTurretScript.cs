using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserTurretScript : MonoBehaviour
{
    public Transform ballons;

    public Transform target;
    public Transform shootPoint;

    public Transform rotatePart;

    public float range = 15f;
    public float firingRate = 0.8f;

    private int damage = 5;

    private float countTime = 0;



    // Start is called before the first frame update
    void Start()
    {
        
        //InvokeRepeating(nameof(FindTarget), 0, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) > range)
        {
            target = null;
        }

        if (target == null)
        {
            damage = 5;
            FindTarget();
        }

        if (target == null) return;



        if (countTime >= firingRate)
        {
            target.GetComponent<BallonScript>().Damage(damage);
            damage += 5;
            countTime = 0;
        }
        countTime += Time.deltaTime;



        //rotate the turret
        rotatePart.rotation = Quaternion.LookRotation(target.position - rotatePart.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void FindTarget()
    {
        if (ballons == null)
            return;
        float min = range + 1;
        Transform save = null;
        for (int i = 0; i < ballons.childCount; i++)
        {
            float distance = Vector3.Distance(transform.position, ballons.GetChild(i).position);
            if (distance < min)
            {
                save = ballons.GetChild(i);
                min = distance;
            }
        }

        if (min < range)
        {
            target = save;
            return;
        }
        target = null;
    }

}
