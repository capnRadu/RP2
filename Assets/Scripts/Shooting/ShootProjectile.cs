using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject point;
    [SerializeField] private float speed;
    public string projectileName;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(point, null);
        projectile.GetComponent<Projectile>().projectileName = projectileName;
        projectile.transform.position = this.transform.position;
        projectile.GetComponent<Rigidbody2D>().AddRelativeForce(-this.transform.right * speed);
    }
}
