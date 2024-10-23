using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGreen : Enemy
{
    [SerializeField] private float _speed;
    [SerializeField] private float _shootInterval = 2.0f;
    [SerializeField] GameObject cannon;

    [SerializeField] GameObject bulletPrefab;

    private float _shootTimer = 0;

    private void Start()
    {
        _rigidbody.linearVelocity = Vector2.down * _speed;
    }

    private void Update()
    {
        _shootTimer += Time.deltaTime;
        if (_shootTimer >= _shootInterval)
        {
            Instantiate(bulletPrefab, cannon.transform.position, Quaternion.identity);
            _shootTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerStats>().PlayerTakeDamage(_damage);
            Instantiate(_explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public override void DeathSequence()
    {
        base.DeathSequence();
        Instantiate(_explosionPrefab, transform.position, transform.rotation);
    }

    public override void HurtSequence()
    {
        Instantiate(_explosionPrefab, transform.position, transform.rotation);
        _animator.SetTrigger("Damage");
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}