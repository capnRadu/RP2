using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    [SerializeField] private Reticle reticleManager;
    private bool canShoot = true;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (canShoot)
        {
            sprite.color = new Color(1f, 1f, 1f, 0.2f);
            reticleManager.Selected(this.gameObject);
        }
    }

    private void OnMouseUp()
    {
        if (canShoot)
        {
            sprite.color = new Color(1f, 1f, 1f, 1f);
            reticleManager.Deselect();
        }
    }

    public void DisableShooting()
    {
        canShoot = false;
        sprite.color = Color.red;
        StartCoroutine(EnableShooting());
    }

    private IEnumerator EnableShooting()
    {
        yield return new WaitForSeconds(2f);
        canShoot = true;
        sprite.color = new Color(1f, 1f, 1f, 1f);
    }
}
