using UnityEngine;

public class BossDeath : BossBaseState
{
    public override void RunState()
    {
        EndGameManager.instance._possibleWin = true;
        EndGameManager.instance.StartResolveSequence();
        gameObject.SetActive(false);
    }
}
