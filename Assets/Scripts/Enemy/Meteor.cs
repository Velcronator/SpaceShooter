using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Enemy
{
    [SerializeField] protected float _minSpeed;
    [SerializeField] protected float _maxSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private PrefabSO _powerUpSpawner;


    private float _speed;
    private bool _isRotating;

    private void Start()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
        _rigidbody.linearVelocity = Vector2.down * _speed;

        _isRotating = true;
        StartCoroutine(RotateMeteor());
    }

    private IEnumerator RotateMeteor()
    {
        float randomRotationSpeed = Random.Range(-_rotationSpeed, _rotationSpeed);
        while (_isRotating)
        {
            transform.Rotate(0, 0, randomRotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public override void HurtSequence()
    {
        //Debug.Log("Meteor hurt!");
    }

    override public void DeathSequence()
    {
        if (_powerUpSpawner != null)
        {
            _powerUpSpawner.SpawnPowerUps(transform.position);
        }
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        base.DeathSequence();
    }

    private void OnTriggerEnter2D(Collider2D otherColl)
    {
        if (otherColl.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerStats player = otherColl.GetComponent<PlayerStats>();
            if (player != null)
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                player.PlayerTakeDamage(_damage);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
