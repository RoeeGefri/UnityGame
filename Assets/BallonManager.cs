using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonManager : MonoBehaviour
{
    public GameObject ballonObject;
    public Transform wayPoints;

    private Vector3 startPoint;

    public int level = 1;
    private int ballonCount = 2;

    private float timeCount = 0;
    public float ballonDelay = 0.25f;




    // Start is called before the first frame update
    void Start()
    {
        startPoint = wayPoints.GetChild(0).position;
    }

    // Update is called once per frame
    void Update()
    {
        
        GameObject go;
        
        timeCount += Time.deltaTime;

        if(ballonCount == 0 && transform.childCount == 0)
        {
            level++;
            ballonCount = level*2;
        }

        if(ballonCount > 0 && timeCount > ballonDelay)
        {
            timeCount = 0;
            ballonCount--;
            go = Instantiate(ballonObject,startPoint,Quaternion.identity,transform);
            go.GetComponent<BallonScript>().SetWayPoints(wayPoints);
        }
        
        
    }
}
