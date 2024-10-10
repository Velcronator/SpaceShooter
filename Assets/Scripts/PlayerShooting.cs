using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootingInterval;

    private float _intervalReset;


    void Start()
    {
        _intervalReset = _shootingInterval;
    }

    // Update is called once per frame
    void Update()
    {
        _shootingInterval -= Time.deltaTime;
        if (_shootingInterval <= 0)
        {
            Shoot();
            _shootingInterval = _intervalReset;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(_firePoint.up * _bulletSpeed, ForceMode2D.Impulse);
    }
}
