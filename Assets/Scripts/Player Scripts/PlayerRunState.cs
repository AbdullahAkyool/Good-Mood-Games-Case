using UnityEngine;

public class PlayerRunState : IState
{
    private PlayerController player;

    public PlayerRunState(PlayerController player)
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

        if (input.sqrMagnitude <= 0.01f)
        {
            player.stateMachine.ChangeState(new PlayerIdleState(player));
            return;
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            player.stateMachine.ChangeState(new PlayerMoveState(player));
            return;
        }

        player.UpdateMovement(input, true);

        if (Input.GetMouseButtonDown(0))
        {
            player.stateMachine.ChangeState(new PlayerAttackState(player));
        }
    }

    public void StateExit() { }
}
