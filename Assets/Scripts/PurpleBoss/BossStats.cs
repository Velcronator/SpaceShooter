using UnityEngine;

public class BossStats : Enemy
{
    [SerializeField] BossController _bossController;

    public override void HurtSequence()
    {
        if(_animator.GetCurrentAnimatorStateInfo(0).IsTag("Dmg")) return;
        _animator.SetTrigger("Damage");
    }

    public override void DeathSequence()
    {
        base.DeathSequence();
        _bossController.ChangeState(BossState.death);
        Instantiate(_explosionPrefab, transform.position, Quaternion.Euler(0,0,Random.Range(0,360)));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().PlayerTakeDamage(_damage);
        }
    }
}
