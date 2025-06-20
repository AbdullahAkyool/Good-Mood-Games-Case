using UnityEngine;

public class PlayerAttackState : IState
{
    private PlayerController player;
    private int attackCount = 5;

    private int currentCombo = 0;
    private float necessaryInputTime = 0.7f;
    private float timer = 0f;
    private bool comboQueued = false;
    private bool animationFinished = false;

    private ProgressBarController progressBarController;

    public PlayerAttackState(PlayerController player)
    {
        this.player = player;
        progressBarController = UIManager.Instance.ProgressBarController;
    }

    public void StateEnter()
    {
        PlayAttackAnimation();
    }

    public void StateUpdate()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timer <= necessaryInputTime)
        {
            comboQueued = true;
        }

        if (animationFinished)
        {
            if (comboQueued && currentCombo < 2)
            {
                currentCombo++;
                comboQueued = false;
                animationFinished = false;
                timer = 0f;
                PlayAttackAnimation();
            }
            else
            {
                player.stateMachine.ChangeState(new PlayerIdleState(player));
            }
        }
    }

    public void StateExit()
    {
        comboQueued = false;
        animationFinished = false;
        currentCombo = 0;
    }

    private void PlayAttackAnimation()
    {
        int randomIndex = Random.Range(0, attackCount);
        player.Animator.SetInteger("AttackIndex", randomIndex);
        player.Animator.SetTrigger("Attack");

        // Combo UI
        if (currentCombo == 0)
        {
            ActionManager.OnShowAttackMessage("CRITICAL HIT!");
        }
        else if (currentCombo == 1)
        {
            ActionManager.OnShowAttackMessage("x2 COMBO!");
        }
        else if (currentCombo == 2)
        {
            ActionManager.OnShowAttackMessage("x3 COMBO!");
        }

        progressBarController.ShowProgressBar(necessaryInputTime);
    }

    public void OnAttackAnimationEnd()
    {
        animationFinished = true;

        progressBarController.HideProgressBar();
    }
}


