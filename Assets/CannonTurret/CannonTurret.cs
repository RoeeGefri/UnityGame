using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CannonTurret : MonoBehaviour
{
    public Transform ballons;
    public GameObject bullet;

    public Transform target = null;

    public Transform shootPoint;
    public Transform bulletStack;
    public Transform rotatePart;


    public float minRange = 10f;
    public float maxRange = 20f;
    public float firingRate = 1f;

    private float countTime = 0;



    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating(nameof(FindTarget), 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject go;

        // bullet part
        if(target != null && (Vector3.Distance(transform.position,target.position) > maxRange || Vector3.Distance(transform.position, target.position) < minRange))
        {
            target = null;
        }
        if(target == null) FindTarget();
        if (target == null) return;

        if (countTime >= firingRate)
        {
            go = Instantiate(bullet, shootPoint.position, Quaternion.identity, bulletStack);
            go.GetComponent<CannonBullet>().SetTarget(target);
            go.GetComponent<CannonBullet>().SetBallons(ballons);
            countTime = 0;
        }
        countTime += Time.deltaTime;

        rotatePart.rotation = Quaternion.LookRotation(target.position - transform.position);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minRange);
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }

    void FindTarget()
    {
        if(ballons == null)
        {
            return;
        }
        float min = maxRange + 1;
        Transform save = null;
        for (int i = 0; i < ballons.childCount; i++)
        {
            float distance = Vector3.Distance(transform.position, ballons.GetChild(i).position);
            if (distance < min && distance < maxRange && distance > minRange)
            {
                save = ballons.GetChild(i);
                min = distance;
            }
        }

        target = save;
    }
}
