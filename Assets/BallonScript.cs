using UnityEngine;

public class BallonScript : MonoBehaviour
{
    public Vector3 target;
    private float ballonMovementSpeed = 10;

    private Transform wayPoints;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = (target - transform.position).normalized * ballonMovementSpeed * Time.deltaTime;
        transform.Translate(move);

        if (Vector3.Distance(transform.position, target) < 0.25)
        {
            target = GetNextPoint(target);
        }
    }

    public Vector3 GetNextPoint(Vector3 current)
    {
        int save = 0;
        
        for(int i = 0; i < wayPoints.childCount; i++)
        {
            if(wayPoints.GetChild(i).position == current)
            {
                if(i == wayPoints.childCount - 1)
                {
                    Destroy(gameObject);
                    return new Vector3(0,0,0);
                }
                save = i + 1;
            }
        }
        return wayPoints.GetChild(save).position;
    }


    public void SetWayPoints(Transform obj)
    {
        wayPoints = obj;
        SetTarget(wayPoints.GetChild(1).position);
    }

    public void SetTarget(Vector3 des)
    {
        target = des;
    }

    public void SetSpeed(float speed)
    {
        ballonMovementSpeed = speed;
    }

}
