using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }

    [SerializeField] private Projectile projectilePrefab;

    private ObjectPool<Projectile> _pool;

    private void Start()
    {
        Instance = this;
        _pool = new(InstantiateProjectile,TakeFromPool,ReturnToPool);
    }

    private Projectile InstantiateProjectile()
    {
        return Instantiate(projectilePrefab);
    }

    private void TakeFromPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
    }

    private void ReturnToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    public Projectile GetObject()
    {
        return _pool.Get();
    }
    public void ReturnObject(Projectile projectile)
    {
        _pool.Release(projectile);
    }
}
