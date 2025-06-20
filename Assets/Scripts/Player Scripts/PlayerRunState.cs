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
         Vector2 input = player.GetInputRaw();

        if (input.magnitude <= 0.1f)
        {
            player.stateMachine.ChangeState(new PlayerIdleState(player));
            return;
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            player.stateMachine.ChangeState(new PlayerMoveState(player));
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            player.stateMachine.ChangeState(new PlayerAttackState(player));
            return;
        }
        
        player.UpdateMovement(input, isRunning: true);
    }

    public void StateExit() { }
}
