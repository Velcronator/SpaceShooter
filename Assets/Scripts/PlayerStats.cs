using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private Image _healthFill;


    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        _healthFill.fillAmount = _currentHealth / _maxHealth;
    }

    public void PlayerTakeDamage(float damage)
    {
        _currentHealth -= damage;
        _healthFill.fillAmount = _currentHealth / _maxHealth;
        if (_currentHealth <= 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        Destroy(gameObject);
    }
}
