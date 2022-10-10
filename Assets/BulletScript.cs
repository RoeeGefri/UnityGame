using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Transform target;

    private float bulletSpeed = 70f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.Translate((target.position-transform.position).normalized * bulletSpeed * Time.deltaTime);

        if (Vector3.Distance(target.position, transform.position) < 0.25)
        {
            Destroy(target.gameObject);
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform ballon)
    {
        target = ballon;
    }

}
