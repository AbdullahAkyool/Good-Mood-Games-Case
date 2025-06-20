using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }

    private CharacterController characterController;
    public CharacterController CharacterController => characterController;

    private Animator animator;
    public Animator Animator => animator;

    [SerializeField] private MovementTypeEnum movementType = MovementTypeEnum.Directional;
    public MovementTypeEnum MovementType => movementType;

    public float moveSpeed = 2f;
    private Vector2 moveInput;

    [SerializeField] private AttackZoneController attackZoneController;
    public AttackZoneController AttackZoneController => attackZoneController;

    [SerializeField] private Sword sword;
    public Sword Sword => sword;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        stateMachine = new StateMachine();
    }

    private void Start()
    {
        stateMachine.ChangeState(new PlayerIdleState(this));
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void UpdateMovement(Vector2 input, bool isRunning = false)
    {
        moveInput = input;

        float speedMultiplier = isRunning ? 1.5f : 1f;

        Vector3 move = Vector3.zero;

        // kameranin yonu
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        if (movementType == MovementTypeEnum.Directional)
        {
            // hedef yon
            Vector3 desiredDirection = camForward * input.y + camRight * input.x;

            // hedef yone don
            if (desiredDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(desiredDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            }

            // sadece forward walk anim
            animator.SetFloat("InputX", 0f, 0.1f, Time.deltaTime);
            animator.SetFloat("InputY", input.magnitude > 0.01f ? 1f * speedMultiplier : 0f, 0.1f, Time.deltaTime);

            move = transform.forward;
        }
        else if (movementType == MovementTypeEnum.Strafe)
        {
            //hedef yon
            move = camForward * input.y + camRight * input.x;

            //kameranin baktigi yone
            if (move.sqrMagnitude > 0.01f)
            {
                Vector3 lookDir = Camera.main.transform.forward;
                lookDir.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), 10f * Time.deltaTime);
            }

            animator.SetFloat("InputX", input.x, 0.1f, Time.deltaTime);
            animator.SetFloat("InputY", input.y * speedMultiplier, 0.1f, Time.deltaTime);

        }

        characterController.Move(move.normalized * moveSpeed * speedMultiplier * Time.deltaTime);
    }
    public Vector2 GetInputRaw()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public void OnAttackEnd() // animation event
    {
        //stateMachine.ChangeState(new PlayerIdleState(this));

        if (stateMachine != null && stateMachine.CurrentState is PlayerAttackState playerAttackState)
        {
            playerAttackState.OnAttackAnimationEnd();
        }
    }

    public void AttackHit() // animation event
    {
        if (stateMachine != null && stateMachine.CurrentState is PlayerAttackState playerAttackState)
        {
            playerAttackState.OnAttackAnimationHit(attackZoneController.EnemiesOnTarget);
        }
    }
}
