using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;

    [Header("General")]
    public float range = 15f;
    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;


    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turretSpeed = 10f;

    public Transform firePoint;

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
        {
            // if the target is null
            // and use laser is enabled
            // then we disable / hide the laser/ line renderer
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {

            if (fireCountdown <= 0f)
            {
                Shoot();
                // more the fire rate faster 
                // the next bullet will shoot
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

    }

    void Laser()
    {
        if (!lineRenderer.enabled)
            lineRenderer.enabled = true;
        // STARTING POSITION OF LASER
        lineRenderer.SetPosition(0, firePoint.position);
        // END POSITION OF LASER
        lineRenderer.SetPosition(1, target.position);
    }

    void LockOnTarget()
    {

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

    void Shoot()
    {
        // typecasting to GameObject type
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
