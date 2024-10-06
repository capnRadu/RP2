using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefObstacle : MonoBehaviour
{
    [NonSerialized] public Transform destroyPoint;
    [NonSerialized] public float moveSpeed;

    [NonSerialized] public Clickable reticleSpawner;

    private void Update()
    {
        if (destroyPoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, destroyPoint.position, moveSpeed * Time.deltaTime);
        }

        if (transform.position.x == destroyPoint.position.x)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            reticleSpawner.DisableShooting();
        }
    }
}
