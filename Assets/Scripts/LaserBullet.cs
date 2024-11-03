using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class LaserBullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private Rigidbody2D _rb;
    private ObjectPool<LaserBullet> m_referencePool;

    void OnEnable()
    {
        _rb.linearVelocity = transform.up * _speed;
    }

    public void SetPool(ObjectPool<LaserBullet> pool)
    {
        m_referencePool = pool;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(_damage);
            if(gameObject.activeSelf)
                m_referencePool.Release(this);
        }
    }

    private void OnDisable()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void SetDirectionAndSpeed()
    {
        _rb.linearVelocity = transform.up * _speed;
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
            m_referencePool.Release(this);
    }
}
