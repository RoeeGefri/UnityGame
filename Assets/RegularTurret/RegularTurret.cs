
using UnityEngine;

public class RegularTurret : MonoBehaviour
{

    public Transform ballons;
    public GameObject bullet;

    public Transform target = null;
    public Transform shootPoint;
    public Transform bulletStack;

    public Transform rotatePart;
    public Transform rotateShoot;


    public float range = 15f;
    public float firingRate = 0.1f;

    private float countTime = 0;
    


    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating(nameof(FindTarget), 0f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject go;
        if(target != null && Vector3.Distance(transform.position, target.position) > range)
        {
            target = null;
        }
        if (target == null) FindTarget();
        // bullet part
        if (target == null) return;

        if(countTime >= firingRate)
        {
            rotateShoot.Rotate(0, 0, 45);
            go = Instantiate(bullet, shootPoint.position, Quaternion.identity, bulletStack);
            go.GetComponent<BulletScript>().SetTarget(target);
            countTime = 0;
        }
        countTime += Time.deltaTime;



        //rotate the turret
        rotatePart.rotation = Quaternion.LookRotation(target.position-rotatePart.position);
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
