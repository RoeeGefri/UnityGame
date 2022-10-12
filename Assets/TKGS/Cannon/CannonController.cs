using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public float rotationSpeed = 1;
    public float BlastPower = 5;

    public GameObject Cannonball;
    public Transform ShotPoint;
    public Transform cannon;
    public Transform target;



    public GameObject Explosion;
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3((target.position.x - transform.position.x),0, (target.position.z - transform.position.z)));

        //cannon.rotation = Quaternion.Euler(cannon.rotation.eulerAngles + new Vector3(VericalRotation * rotationSpeed, 0f, 0f));
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject CreatedCannonball = Instantiate(Cannonball, ShotPoint.position, ShotPoint.rotation);
            CreatedCannonball.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * BlastPower;

            // Added explosion for added effect
            //Destroy(Instantiate(Explosion, ShotPoint.position, ShotPoint.rotation), 2);


        }
    }


}