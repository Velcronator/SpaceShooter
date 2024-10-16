using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private int _hitsToDestroy = 3;
    public bool _protection = false;

    private void OnEnable()
    {
        _hitsToDestroy = 3;
        _protection = true;
    }

    private void DamageShield()
    {
        _hitsToDestroy--;
        if (_hitsToDestroy <= 0)
        {
            _protection = false;
            gameObject.SetActive(false);
        }
    }

    public void RepairShield()
    {
        _hitsToDestroy = 3;
    }

    private void OnTriggerEnter2D(Collider2D otherColl)
    {
        if (otherColl.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(1000);
            DamageShield();
        }
        else
        {
            Destroy(otherColl.gameObject);
            DamageShield();
        }
    }
}
