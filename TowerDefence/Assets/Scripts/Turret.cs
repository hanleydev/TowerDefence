using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;

    [Header("Attributes")]

    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Config")]

    public string enemyTag = "Enemy";

    public Transform towerRotation;
    public float rotationSpeed = 10f;

    public GameObject turretProjectilePrefab;
    public Transform firePoint;



    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanecToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanecToEnemy < shortestDistance)
            {
                shortestDistance = distanecToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else {
            target = null;
        }

    }

    void Update()
    {
        if (target == null)
            return;

        //Method below on how the tower turns to face targets
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(towerRotation.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        towerRotation.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        //Shooting fire rate and cooldown
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
        
    }

    void Shoot()
    {
        GameObject projectileGO = (GameObject)Instantiate(turretProjectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if (projectile != null)
            projectile.Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}