using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserTurretScript : MonoBehaviour
{
    public Transform ballons;

    public Transform[] target = new Transform[5];
    public Transform shootPoint;

    public float range = 15f;
    public float firingRate = 0.3f;

    private float countTime = 0;



    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating(nameof(FindTarget), 0, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {

        // bullet part

        bool allNulls = true;

        for (int i = 0; i < target.Length; i++)
        {
            if(target[i] == null)
            {
                allNulls = false;
            }
        }


        if (!allNulls) return;



        if (countTime >= firingRate)
        {
            for(int i = 0; i < target.Length; i++)
            {
                if (target[i] != null)
                {
                    Destroy(target[i].gameObject);
                }
            }
            countTime = 0;
        }
        countTime += Time.deltaTime;



        //rotate the turret
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void FindTarget()
    {
        for (int k = 0; k < target.Length; k++)
        {
            if (target[k] != null)
            {
                continue;
            }
            float min = range + 1;
            Transform save = null;
            for (int i = 0; i < ballons.childCount; i++)
            {
                float distance = Vector3.Distance(transform.position, ballons.GetChild(i).position);
                if (distance < min && NotExist(ballons.GetChild(i)))
                {
                    save = ballons.GetChild(i);
                    min = distance;
                }
            }


            if (min < range)
            {
                target[k] = save;
                return;
            }
            target[k] = null;
        }
        
    }

    private bool NotExist(Transform other)
    {
        for(int i = 0; i < target.Length; i++)
        {
           if (target[i] == other)
           {
                return false;
            }
        }
        return true;
    }

}