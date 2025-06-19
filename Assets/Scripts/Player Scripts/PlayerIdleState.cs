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
        player.Animator.CrossFade("Movement", 0.1f);
    }

    public void StateUpdate()
    {
        Vector2 input = player.GetInput();
        if (input.sqrMagnitude > 0.01f)
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

