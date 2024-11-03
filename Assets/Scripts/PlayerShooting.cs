using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private LaserBullet m_laserBullet;
    [SerializeField] AudioSource _audioSource;
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

    private ObjectPool<LaserBullet> m_pool;

    private float _intervalReset;

    private void Awake()
    {
        m_pool = new ObjectPool<LaserBullet>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyPool, true, 10, 30);
    }

    private LaserBullet CreateBullet()
    {
        LaserBullet bullet = Instantiate(m_laserBullet, transform.position, transform.rotation);
        bullet.SetPool(m_pool);
        return bullet;
    }

    private void OnTakeBulletFromPool(LaserBullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReturnBulletToPool(LaserBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    void Start()
    {
        _intervalReset = _shootingInterval;
    }

    private void OnDestroyPool(LaserBullet laserBullet)
    {
        Destroy(laserBullet.gameObject);
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
        _audioSource.Play();
        switch (_upgradeLevel)
        {
            case 0:
                //InstantiateBullet(_firePoint.position, _firePoint.rotation);
                m_pool.Get().transform.position = _firePoint.position;
                break;
            case 1:
                //InstantiateBullet(_cannonLeft01.position, _cannonLeft01.rotation);
                //InstantiateBullet(_cannonRight01.position, _cannonRight01.rotation);
                m_pool.Get().transform.position = _cannonLeft01.position;
                m_pool.Get().transform.position = _cannonRight01.position;
                break;
            case 2:
                //InstantiateBullet(_firePoint.position, _firePoint.rotation);
                //InstantiateBullet(_cannonLeft01.position, _cannonLeft01.rotation);
                //InstantiateBullet(_cannonRight01.position, _cannonRight01.rotation);
                m_pool.Get().transform.position = _firePoint.position;
                m_pool.Get().transform.position = _cannonLeft01.position;
                m_pool.Get().transform.position = _cannonRight01.position;

                break;
            case 3:
                //InstantiateBullet(_firePoint.position, _firePoint.rotation);
                //InstantiateBullet(_cannonLeft01.position, _cannonLeft01.rotation);
                //InstantiateBullet(_cannonRight01.position, _cannonRight01.rotation);
                //InstantiateBullet(_cannonLeft02.position, _cannonLeft02.rotation);
                //InstantiateBullet(_cannonRight02.position, _cannonRight02.rotation);
                m_pool.Get().transform.position = _firePoint.position;
                m_pool.Get().transform.position = _cannonLeft01.position;
                m_pool.Get().transform.position = _cannonRight01.position;
                m_pool.Get().transform.position = _cannonLeft02.position;
                m_pool.Get().transform.position = _cannonRight02.position;
                break;
            case 4:
                //InstantiateBullet(_firePoint.position, _firePoint.rotation);
                //InstantiateBullet(_cannonLeft01.position, _cannonLeft01.rotation);
                //InstantiateBullet(_cannonRight01.position, _cannonRight01.rotation);
                //InstantiateBullet(_cannonLeft02.position, _cannonLeft02.rotation);
                //InstantiateBullet(_cannonRight02.position, _cannonRight02.rotation);
                //InstantiateBullet(_cannonLeft03Rotation.position, _cannonLeft03Rotation.rotation);
                //InstantiateBullet(_cannonRight03Rotation.position, _cannonRight03Rotation.rotation);
                m_pool.Get().transform.position = _firePoint.position;
                m_pool.Get().transform.position = _cannonLeft01.position;
                m_pool.Get().transform.position = _cannonRight01.position;
                m_pool.Get().transform.position = _cannonLeft02.position;
                m_pool.Get().transform.position = _cannonRight02.position;

                LaserBullet bullet01 = m_pool.Get();
                bullet01.transform.position = _cannonLeft03Rotation.position;
                bullet01.transform.rotation = _cannonLeft03Rotation.rotation;
                bullet01.SetDirectionAndSpeed();
                LaserBullet bullet02 = m_pool.Get();
                bullet02.transform.position = _cannonRight03Rotation.position;
                bullet02.transform.rotation = _cannonRight03Rotation.rotation;
                bullet02.SetDirectionAndSpeed();
                break;
        }
    }


    private void InstantiateBullet(Vector3 position, Quaternion rotation)
    {
        LaserBullet bullet = Instantiate(m_laserBullet, position, rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.linearVelocity = bullet.transform.forward * _bulletSpeed;
        }
    }

}
