using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase_WithTurning : MonoBehaviour
{
    //public GameObject player;
    public Transform player;
    public float speed;

    private float distance;


    //Gun
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    private bool canShoot = true;
    private float lastShotTime = 0f;
    private readonly float shootInterval = 3f; // how long should we wait between shots

    void Start()
    {
        
    }


    void Update()
    {
        if(!player)GetTarget();

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        if ( canShoot && Time.time > lastShotTime + shootInterval)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        canShoot = false;
        Invoke(nameof(EnableShoot), shootInterval);
    }

    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    void EnableShoot()
    {
        canShoot = true;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            player = null;
        }
    }
}
