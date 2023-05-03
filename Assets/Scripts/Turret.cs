using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    public float range = 15f;
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turretSpeed = 10f;

    void Start()
    {
        // call the function every 0.5f
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        // If we don't get any enemy in range 
        // then the shortest distance will be infinity
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
            return;
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        // convert lookRotation --> x, y, z rotation
        // transitioning from currentRotation toward enemy whenever found new enemy with a speed
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turretSpeed).eulerAngles;
        // only want to rotate in y direction 
        // instead in all direction
        // Note change the partToRotate object Y Rotation --> so that
        // it point toward turret gun
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0);

    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
