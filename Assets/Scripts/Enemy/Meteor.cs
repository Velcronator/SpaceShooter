using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class Meteor : Enemy
{
    [SerializeField] protected float _minSpeed;
    [SerializeField] protected float _maxSpeed;

    private float _speed;

    private void Start()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
        _rigidbody.velocity = Vector2.down * _speed;
    }

    public override void HurtSequence()
    {
        Debug.Log("Meteor hurt!");
    }

    override public void DeathSequence()
    {
        Debug.Log("Meteor died!");
    }

    private void OnTriggerEnter2D(Collider2D otherColl)
    {
        if(otherColl.CompareTag("Player"))
        {
            // destroy the player
            Destroy(otherColl.gameObject);
        }
    }
}
