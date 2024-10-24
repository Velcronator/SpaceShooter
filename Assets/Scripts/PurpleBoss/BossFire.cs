using System.Collections;
using UnityEngine;

public class BossFire : BossBaseState
{
    [SerializeField] private float _speed;
    [SerializeField] private float _shootRate;
    [SerializeField] private GameObject _bulletPrefab;

    [Header("Shooting Points")]
    [SerializeField] private Transform [] _shootPoints;

    public override void RunState()
    {
        StartCoroutine(RunFireState());
    }

    public override void StopState()
    {
        base.StopState();
    }


    IEnumerator RunFireState()
    {
        float shootTimer = 0f;
        float fireStateTimer = 0f;
        float fireStateExitTime = Random.Range(5f, 10f);
        Vector2 targetPosition = new Vector2(Random.Range(_maxLeft, _maxRight), Random.Range(_maxDown,_maxUp));

        while (fireStateTimer < fireStateExitTime)
        {
            if(Vector2.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
            }
            else
            {
                targetPosition = new Vector2(Random.Range(_maxLeft, _maxRight), Random.Range(_maxDown,_maxUp));
            }

            shootTimer += Time.deltaTime;
            fireStateTimer += Time.deltaTime;

            if (shootTimer >= _shootRate)
            {
                Shoot();
                shootTimer = 0f;
            }

            yield return new WaitForEndOfFrame();
        }

        int random = Random.Range(0, 2);
        if(random == 0)
            _bossController.ChangeState(BossState.fire);
        else
            _bossController.ChangeState(BossState.special);
    }

    private void Shoot()
    {
        for(int i = 0; i < _shootPoints.Length; i++)
        {
            Instantiate(_bulletPrefab, _shootPoints[i].position, Quaternion.identity);
        }
    }
}
