using UnityEngine;

public class PlayerAttackState : IState
{
    private PlayerController player;
    private int attackCount = 5;

    public PlayerAttackState(PlayerController player)
    {
        this.player = player;
    }

    public void StateEnter()
    {
        int randomIndex = Random.Range(0, attackCount);
        player.Animator.SetInteger("AttackIndex", randomIndex);
        player.Animator.SetTrigger("Attack");
    }

    public void StateUpdate()
    {
        // animation event
    }

    public void StateExit() { }
}

