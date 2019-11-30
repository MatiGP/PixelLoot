﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemProjectile : EnemyProjectile
{
    public GameObject GolemShrapnelProjectiles;
    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }


    void DestroyProjectile()
    {
        Instantiate(onHitParticle, transform.position, Quaternion.identity);
        float rotz = transform.rotation.eulerAngles.z;
        for (int i = 1; i <= 5; i++)
        {
            Instantiate(GolemShrapnelProjectiles, transform.position, Quaternion.Euler(0,0,rotz -90 + i * 30)) ;
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().TakeDamage(damage);
            DestroyProjectile();
        }
        if (collision.CompareTag("Wall"))
        {
            DestroyProjectile();
        }

    }
}