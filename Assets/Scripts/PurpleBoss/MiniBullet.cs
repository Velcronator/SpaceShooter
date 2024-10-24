using UnityEngine;

public class MiniBullet : MonoBehaviour
{
    [SerializeField] private float _damage = 1;
    [SerializeField] private float _speed = 5;
    [SerializeField] private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody.linearVelocity = transform.up * _speed;
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
