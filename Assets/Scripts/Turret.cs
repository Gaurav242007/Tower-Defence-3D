using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    // Enemy is the enemy script
    private Enemy targetEnemy;


    [Header("General")]
    public float range = 15f;
    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public int damageOverTime = 30;
    public float slowPct = .5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;


    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turretSpeed = 10f;

    public Transform firePoint;

    public AudioSource shootAudio;

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
            targetEnemy = target.GetComponent<Enemy>();
        }
        else
        {
            target = null;
            shootAudio.Stop();
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
                    impactEffect.Stop();
                    impactLight.enabled = false;
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
        shootAudio.Play();
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPct);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        // STARTING POSITION OF LASER
        lineRenderer.SetPosition(0, firePoint.position);
        // END POSITION OF LASER
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        // making so that 
        // particle doesn't originate from centre
        // but away from the radius 
        // to point toward the laster / beamer
        impactEffect.transform.position = target.position + dir.normalized;
        // MAKING IMPACT EFFECT POINT TOWARDS LASER
        // Quaternion.LookRotation takes direction and then it looks
        // in that direction
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
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
        shootAudio.Play();
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
