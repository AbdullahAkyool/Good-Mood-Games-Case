using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyHealthController enemyHealthController;

    private Animator animator;
    public Animator Animator => animator;

    public StateMachine stateMachine { get; private set; }

    private void Awake()
    {
        enemyHealthController = GetComponent<EnemyHealthController>();
        animator = GetComponent<Animator>();
        stateMachine = new StateMachine();
    }

    private void Start()
    {
        stateMachine.ChangeState(new EnemyIdleState(this));
    }

}
