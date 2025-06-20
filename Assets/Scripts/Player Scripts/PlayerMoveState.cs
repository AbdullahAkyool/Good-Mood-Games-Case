using UnityEngine;

public class PlayerMoveState : IState
{
    private PlayerController player;

    public PlayerMoveState(PlayerController player)
    {
        this.player = player;
    }

    public void StateEnter()
    {
        player.Animator.CrossFade("Movement", 0.1f);
    }

    public void StateUpdate()
    {
        Vector2 input = player.GetInputRaw();

        if (input.magnitude <= 0.1f)
        {
            player.stateMachine.ChangeState(new PlayerIdleState(player));
            return;
        }

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        if (isRunning)
        {
            player.stateMachine.ChangeState(new PlayerRunState(player));
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            player.stateMachine.ChangeState(new PlayerAttackState(player));
            return;
        }

        player.UpdateMovement(input, isRunning);
    }

    public void StateExit() { }
}
