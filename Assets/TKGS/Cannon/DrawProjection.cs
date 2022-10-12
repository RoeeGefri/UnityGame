using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawProjection : MonoBehaviour
{
    CannonController cannonController;
    LineRenderer lineRenderer;
    public Transform cannon;
    public Transform target;
    public const float MAX_RANGE = 38f;
    public const float MIN_RANGE = 5f;

    Quaternion originalCannonRotation;

    // Number of points on the line
    public int numPoints = 50;

    // distance between those points on the line
    public float timeBetweenPoints = 0.1f;

    // The physics layers that will cause the line to stop being drawn
    public LayerMask CollidableLayers;
    void Start()
    {
        cannonController = GetComponent<CannonController>();
        lineRenderer = GetComponent<LineRenderer>();
        originalCannonRotation = cannon.rotation;
    }


    void Update()
    {
        lineRenderer.positionCount = (int)numPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = cannonController.ShotPoint.position;
        Vector3 startingVelocity = cannonController.ShotPoint.up * cannonController.BlastPower;
        for (float t = 0; t < numPoints; t += timeBetweenPoints)
        {
            Vector3 newPoint = startingPosition + t * startingVelocity;
            newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
            points.Add(newPoint);

            if (Physics.OverlapSphere(newPoint, 2, CollidableLayers).Length > 0)
            {
                lineRenderer.positionCount = points.Count;
                break;
            }
        }
        Vector3[] pointsArr = points.ToArray();

        float cannonX = transform.position.x;
        float cannonY = transform.position.z;
        float targetX = target.position.x;
        float targetY = target.position.z;
        float shotPointX = pointsArr[pointsArr.Length - 1].x;
        float shotPointY = pointsArr[pointsArr.Length - 1].z;

        float distanceToTarget = Mathf.Sqrt((cannonX - targetX) * (cannonX - targetX) + (cannonY - targetY) * (cannonY - targetY));
        float distanceToShotPoint = Mathf.Sqrt((cannonX - shotPointX) * (cannonX - shotPointX) + (cannonY - shotPointY) * (cannonY - shotPointY));
        //distanceToShotPoint;


        if (distanceToShotPoint <= MAX_RANGE && distanceToShotPoint >= MIN_RANGE)
        {
            if (distanceToShotPoint > distanceToTarget + 0.3f)
            {
                // rotate down
                cannon.Rotate(new Vector3(1, 0, 0) * (100f * Time.deltaTime));
            }
            else if (distanceToShotPoint + 0.3f < distanceToTarget)
            {
                // rotate up
                cannon.Rotate(new Vector3(-1, 0, 0) * (100f * Time.deltaTime));
            }
            else
            {

            }
        }

        else if (distanceToShotPoint > MAX_RANGE)
        {
            // rotate down
            cannon.Rotate(new Vector3(1, 0, 0) * (100f * Time.deltaTime));
        }
            




        //lineRenderer.SetPositions(pointsArr);
    }
}