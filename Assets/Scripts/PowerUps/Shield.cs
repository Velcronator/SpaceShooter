using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private int _hitsToDestroy = 3;
    public bool _protection = false;

    [SerializeField] GameObject[] _shieldBase;

    private void OnEnable()
    {
        _hitsToDestroy = 3;
        for(int i = 0; i < _shieldBase.Length; i++)
        {
            _shieldBase[i].SetActive(true);
        }
        _protection = true;
    }

    private void UpdateUI()
    {
        for (int i = 0; i < _shieldBase.Length; i++)
        {
            _shieldBase[i].SetActive(i < _hitsToDestroy);
        }
        // Can use Switch statement instead of for loop
        
    }

    private void DamageShield()
    {
        _hitsToDestroy--;
        if (_hitsToDestroy <= 0)
        {
            _hitsToDestroy = 0;
            _protection = false;
            gameObject.SetActive(false);
        }
        UpdateUI();
    }

    public void RepairShield()
    {
        _hitsToDestroy = 3;
        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D otherColl)
    {
        if (otherColl.TryGetComponent(out Enemy enemy))
        {
            if(otherColl.CompareTag("Boss"))
            {
                _hitsToDestroy = 0;
                DamageShield();
                return;
            }
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
