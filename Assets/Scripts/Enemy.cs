using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        // referencing the waypoints script static array points
        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        // normalizing the dir so to have fixed speed
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // if the distance between current position and current target waypoint 
        // is less than some small distance then we have reached our waypoint
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
}
