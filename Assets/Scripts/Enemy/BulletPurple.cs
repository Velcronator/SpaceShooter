using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPurple : MonoBehaviour
{
    [SerializeField] private float _damage = 1;
    [SerializeField] private float _speed = 5;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = Vector2.down * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerStats>().PlayerTakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
