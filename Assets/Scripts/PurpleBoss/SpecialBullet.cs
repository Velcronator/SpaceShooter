using System.Collections;
using UnityEngine;

public class SpecialBullet : MonoBehaviour
{

    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] Rigidbody2D _rb;

    [SerializeField] private GameObject _miniBullet;
    [SerializeField] private Transform[] _spawnPoints;

    void Start()
    {
        _rb.linearVelocity = Vector2.down * _speed;
        StartCoroutine(Explode());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    IEnumerator Explode()
    {
        float randomTime = Random.Range(1.5f, 2.5f);
        yield return new WaitForSeconds(randomTime);

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            Instantiate(_miniBullet, _spawnPoints[i].position, _spawnPoints[i].rotation);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().PlayerTakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
