using UnityEngine;

public class EnemyIdleState : IState
{
    private EnemyController enemy;
    
    public EnemyIdleState(EnemyController enemy)
    {
        this.enemy = enemy;
    }
    public void StateEnter()
    {
        enemy.Animator.CrossFade("Idle", 0.1f);
        enemy.Animator.ResetTrigger("GetHit");
    }
    public void StateUpdate(){}

    public void StateExit(){}
}
