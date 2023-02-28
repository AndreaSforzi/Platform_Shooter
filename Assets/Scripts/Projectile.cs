using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
            Destroy(collision.gameObject);

        ProjectilePool.Instance.ReturnObject(this);
    }
}
