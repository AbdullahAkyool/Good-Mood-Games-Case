using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyHealthController enemyHealthController;
    public EnemyHealthController EnemyHealthController => enemyHealthController;

    private Animator animator;
    public Animator Animator => animator;

    private Collider coll;
    public Collider Coll => coll;

    public StateMachine stateMachine { get; private set; }

    [SerializeField] private Renderer meshRenderer;
    public Renderer MeshRenderer => meshRenderer;

    public Rigidbody[] RagdollRigidbodies { get; private set; }

    private void Awake()
    {
        enemyHealthController = GetComponent<EnemyHealthController>();
        coll = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        stateMachine = new StateMachine();

        RagdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in RagdollRigidbodies)
            rb.isKinematic = true;
    }

    private void Start()
    {
        stateMachine.ChangeState(new EnemyIdleState(this));
    }

    public void EnableRagdoll()
    {
        foreach (var rb in RagdollRigidbodies)
        {
            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

}
