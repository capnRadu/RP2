using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    [SerializeField] private Reticle reticleManager;
    private bool canShoot = true;

    private void OnMouseDown()
    {
        if (canShoot)
        {
            reticleManager.Selected(this.gameObject);
        }
    }

    private void OnMouseUp()
    {
        if (canShoot)
        {
            reticleManager.Deselect();
        }
    }

    public void DisableShooting()
    {
        canShoot = false;
        StartCoroutine(EnableShooting());
    }

    private IEnumerator EnableShooting()
    {
        yield return new WaitForSeconds(2f);
        canShoot = true;
    }
}
