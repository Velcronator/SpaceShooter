using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootingInterval;


    [Header("Fire Point")]
    [SerializeField] private Transform _firePoint;

    [Header("UpgradePoints")]
    [SerializeField] private Transform _cannonLeft01;
    [SerializeField] private Transform _cannonRight01;
    [SerializeField] private Transform _cannonLeft02;
    [SerializeField] private Transform _cannonRight02;

    [Header("Upgrade Rotation")]
    [SerializeField] private Transform _cannonLeft03Rotation;
    [SerializeField] private Transform _cannonRight03Rotation;

    public int _upgradeLevel = 0;

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

    public void ChangeWeaponUpgradeLevel(int weaponChangeAmount)
    {
        _upgradeLevel += weaponChangeAmount;
        _upgradeLevel = Mathf.Clamp(_upgradeLevel, 0, 4);
    }

    private void Shoot()
    {
        switch (_upgradeLevel)
        {
            case 0:
                InstantiateBullet(_firePoint.position, _firePoint.rotation);
                break;
            case 1:
                InstantiateBullet(_cannonLeft01.position, _cannonLeft01.rotation);
                InstantiateBullet(_cannonRight01.position, _cannonRight01.rotation);
                break;
            case 2:
                InstantiateBullet(_firePoint.position, _firePoint.rotation);
                InstantiateBullet(_cannonLeft01.position, _cannonLeft01.rotation);
                InstantiateBullet(_cannonRight01.position, _cannonRight01.rotation);
                break;
            case 3:
                InstantiateBullet(_firePoint.position, _firePoint.rotation);
                InstantiateBullet(_cannonLeft01.position, _cannonLeft01.rotation);
                InstantiateBullet(_cannonRight01.position, _cannonRight01.rotation);
                InstantiateBullet(_cannonLeft02.position, _cannonLeft02.rotation);
                InstantiateBullet(_cannonRight02.position, _cannonRight02.rotation);
                break;
            case 4:
                InstantiateBullet(_firePoint.position, _firePoint.rotation);
                InstantiateBullet(_cannonLeft01.position, _cannonLeft01.rotation);
                InstantiateBullet(_cannonRight01.position, _cannonRight01.rotation);
                InstantiateBullet(_cannonLeft02.position, _cannonLeft02.rotation);
                InstantiateBullet(_cannonRight02.position, _cannonRight02.rotation);
                InstantiateBullet(_cannonLeft03Rotation.position, _cannonLeft03Rotation.rotation);
                InstantiateBullet(_cannonRight03Rotation.position, _cannonRight03Rotation.rotation);
                break;
        }
    }


    private void InstantiateBullet(Vector3 position, Quaternion rotation)
    {
        GameObject bullet = Instantiate(_bulletPrefab, position, rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.linearVelocity = bullet.transform.forward * _bulletSpeed;
        }
    }

}
