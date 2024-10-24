using UnityEngine;

public enum BossState
{
    enter,
    fire,
    special,
    death
}

public class BossController : MonoBehaviour
{
    [SerializeField] private BossEnter _bossEnter;
    [SerializeField] private BossFire _bossFire;
    [SerializeField] private BossSpecial _bossSpecial;
    [SerializeField] private BossDeath _bossDeath;

    [SerializeField] private bool test;
    [SerializeField] private BossState testState;


    private void Start()
    {
        ChangeState(BossState.enter);
        if(test)
        {
            ChangeState(testState);
        }
    }

    public void ChangeState(BossState bossState)
    {
        switch (bossState)
        {
            case BossState.enter:
                _bossEnter.RunState();
                break;
            case BossState.fire:
                _bossFire.RunState();
                break;
            case BossState.special:
                _bossSpecial.RunState();
                break;
            case BossState.death:
                _bossEnter.StopState();
                _bossFire.StopState();
                _bossSpecial.StopState();
                _bossDeath.RunState();
                break;
        }
    }
}
