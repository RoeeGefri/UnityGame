using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //Destroy(Instantiate(Explosion, ShotPoint.position, ShotPoint.rotation), 2);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 3);
            Destroy(gameObject);
        }
    }
}
