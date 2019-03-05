using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The base for this prototype was made using a tutorial
 * by the Youtuber "Brackeys". It was then modified to match our needs.
 * Link to video series: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0*/

public class Turret : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float range = 100.0f;
    public float fireRate = 1.0f;
    private float fireCountdown = 0.0f;

    [Header("Unity Setup Fields")]
    public float turnSpeed = 10.0f;
    public string enemyTag = "Enemy";
    public GameObject bulletObj;
    public Transform firePoint;

    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public float laserDamage = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
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

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                }
            }

            return;
        }

        // Target lock on
        LockOnTarget();

        if (useLaser)
        {
            Laser();
            if (target != null)
            {
                if (laserDamage <= 0.0f)
                {
                    laserDamage = 3.0f;
                    Destroy(target.gameObject);
                }

                laserDamage -= Time.deltaTime;
            }
            
        }
        else
        {
            if (fireCountdown <= 0.0f)
            {
                Shoot();
                fireCountdown = 1.0f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject bulletShot = (GameObject)Instantiate(bulletObj, firePoint.position, firePoint.rotation);

        Bullet bullet = bulletShot.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void Laser()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
