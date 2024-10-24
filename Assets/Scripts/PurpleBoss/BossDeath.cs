using UnityEngine;

public class BossDeath : BossBaseState
{
    public override void RunState()
    {
        EndGameManager.instance.StartResolveSequence();
        gameObject.SetActive(false);
    }
}
