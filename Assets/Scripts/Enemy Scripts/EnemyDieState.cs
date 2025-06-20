using UnityEngine;

public class EnemyDieState : IState
{
    private EnemyController enemy;
    
    public EnemyDieState(EnemyController enemy)
    {
        this.enemy = enemy;
    }
    public void StateEnter()
    {

    }
    public void StateUpdate()
    {
        
    }

    public void StateExit()
    {

    }
}
