using UnityEngine;
using System.Collections;

// seeking the enemy script in this other script 
// having same component attached to both
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    // then this enemy variable has all the property
    // of the current component Enemy Script
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        // referencing the waypoints script static array points
        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        // normalizing the dir so to have fixed speed
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        // if the distance between current position and current target waypoint 
        // is less than some small distance then we have reached our waypoint
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
        // reset the speed variable every time
        // so the enemy not slow up every time even after getting out of range 
        // from the Laser Beamer
        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
