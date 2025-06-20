using UnityEngine;

public class PlayerIdleState : IState
{
    private PlayerController player;

    public PlayerIdleState(PlayerController player)
    {
        this.player = player;
    }

    public void StateEnter()
    {
        player.Animator.ResetTrigger("Attack");
        player.Animator.CrossFade("Movement", 0.1f);

        player.Animator.SetFloat("InputX", 0f);
        player.Animator.SetFloat("InputY", 0f);
    }

    public void StateUpdate()
    {
        Vector2 input = player.GetInputRaw();
        if (input.magnitude > 0.1f)
        {
            player.stateMachine.ChangeState(new PlayerMoveState(player));
        }
        else if (Input.GetMouseButtonDown(0))
        {
            player.stateMachine.ChangeState(new PlayerAttackState(player));
        }
    }

    public void StateExit() { }
}

