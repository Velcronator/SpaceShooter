using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPurple : Enemy
{
    [SerializeField] private float _speed;
    [SerializeField] private float _shootInterval = 3.0f;
    [SerializeField] GameObject cannonL;
    [SerializeField] GameObject cannonR;

    [SerializeField] GameObject bulletPrefab;

    private float _shootTimer = 0;

    private void Start()
    {
        _rigidbody.velocity = Vector2.down * _speed;
    }

    private void Update()
    {
        _shootTimer += Time.deltaTime;
        if (_shootTimer >= _shootInterval)
        {
            Instantiate(bulletPrefab, cannonL.transform.position, Quaternion.identity);
            Instantiate(bulletPrefab, cannonR.transform.position, Quaternion.identity);
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
        Instantiate(_explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public override void HurtSequence()
    {
        if(_animator.GetCurrentAnimatorStateInfo(0).IsTag("Dmg"))
        {
            Debug.Log("Already playing damage animation");
            return;
        }
        Instantiate(_explosionPrefab, transform.position, transform.rotation);
        _animator.SetTrigger("Damage");
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}