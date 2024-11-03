using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private Image _healthFill;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private Shield _shield;

    private PlayerShooting _playerShooting;

    private bool _canPlayDamageAnimation = true;
    public bool _canTakeDamage = true;

    // Start is called before the first frame update
    void OnEnable()
    {
        _currentHealth = _maxHealth;
        _healthFill.fillAmount = _currentHealth / _maxHealth;
        EndGameManager.instance._gameOver = false;
        StartCoroutine(DamageProtection());
    }

    private void Start()
    {
        _playerShooting = GetComponent<PlayerShooting>();
        EndGameManager.instance.RegisterPlayerStats(this);
        EndGameManager.instance._possibleWin = false;
    }

    IEnumerator DamageProtection()
    {
        _canTakeDamage = false;
        yield return new WaitForSeconds(2f);
        _canTakeDamage = true;
    }

    public void PlayerTakeDamage(float damage)
    {
        if (!_canTakeDamage)
        {
            return;
        }

        if (_shield._protection)
        {   
            return;
        }

        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        _currentHealth -= damage;
        _healthFill.fillAmount = _currentHealth / _maxHealth;

        if (_canPlayDamageAnimation)
        {
            animator.SetTrigger("Damage");
            StartCoroutine(AntiSpamAnimation());
        }

        _playerShooting.ChangeWeaponUpgradeLevel(-1);

        if (_currentHealth <= 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        //Destroy(gameObject);
        EndGameManager.instance._gameOver = true;
        EndGameManager.instance.StartResolveSequence();
        gameObject.SetActive(false);
    }

    private IEnumerator AntiSpamAnimation()
    {
        _canPlayDamageAnimation = false;
        yield return new WaitForSeconds(0.15f);
        _canPlayDamageAnimation = true;
    }

    public void PlayerHeal(int healAmount)
    {
        _currentHealth += healAmount;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        _healthFill.fillAmount = _currentHealth / _maxHealth;

    }
}
