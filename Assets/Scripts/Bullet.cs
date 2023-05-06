using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    public int damage = 50;
    // explosion radius for mission launcher 
    // to explode in a particular area
    // and damages all enemies within it.
    public float explosionRadius = 0f;
    public GameObject impactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        // if the enemy is Damaged
        // or enemy og far
        // then the bullet automatically destroys it self
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        // distance for one frame
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        // Destroy effect after 2 seconds
        Destroy(effectIns, 5f);
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        // destroying the bullet prefab after collision
        Destroy(gameObject);
    }

    void Explode()
    {
        // create a sphere with a radius
        // and check all the colliders
        // within the radius
        // list of colliders (or objects hit)
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            // filtered by tag ==> enemy
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }

    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }

    // viewing the range highlighted
    // in a 3d sphere grid
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
