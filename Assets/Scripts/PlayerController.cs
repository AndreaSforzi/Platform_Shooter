using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float jumpForce = 500;
    [SerializeField] private float shootingForce = 100;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float shotCooldown = 1;

    private bool _isGrounded;
    private float _shotDelay = 0;

   

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(horizontal, 0, 0);

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpForce);
        }

        _shotDelay -= Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            if (_shotDelay <=0)
            { 
                Shoot();
                _shotDelay = shotCooldown;
            }  
        }
    }

    private void FixedUpdate()
    {
        CheckForGround();
    }

    private void Shoot()
    {
        Vector2 shootDirection = GetVector().normalized;
        Projectile newShoot = ProjectilePool.Instance.GetObject();
        newShoot.transform.position = transform.position;
        newShoot.GetComponent<Rigidbody2D>().AddForce(shootDirection * shootingForce);
        
    }

    private Vector2 GetVector()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void CheckForGround()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundMask);
    }
}
