using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float _health;
    [SerializeField] protected Rigidbody2D _rigidbody;

    [SerializeField] protected float _damage;
    [SerializeField] protected GameObject _explosionPrefab;

    [SerializeField] protected Animator _animator;
    [SerializeField] private int _scoreValue;

    void Start()
    {

    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        HurtSequence();
        if (_health <= 0)
        {
            _health = 0;
            DeathSequence();
        }
    }

    public virtual void HurtSequence()
    {
        Debug.Log("Base Enemy Ouch!");
    }

    public virtual void DeathSequence()
    {
        EndGameManager.instance.UpdateScore(_scoreValue);
        Destroy(gameObject);
    }


}
