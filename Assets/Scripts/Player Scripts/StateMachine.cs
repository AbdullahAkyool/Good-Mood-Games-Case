using UnityEngine;

public class StateMachine
{
    private IState currentState;

    public void ChangeState(IState newState)
    {
        currentState?.StateExit();
        currentState = newState;
        currentState?.StateEnter();
    }

    public void Update()
    {
        currentState?.StateUpdate();
    }
}
