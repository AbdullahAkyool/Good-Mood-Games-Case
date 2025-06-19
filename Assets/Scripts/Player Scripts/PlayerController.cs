using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }

    private CharacterController characterController;
    public CharacterController CharacterController => characterController;

    private Animator animator;
    public Animator Animator => animator;

    public float moveSpeed = 2f;
    private Vector2 moveInput;

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
        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y * speedMultiplier);

        Vector3 move = new Vector3(input.x, 0, input.y);
        move = Camera.main.transform.TransformDirection(move);
        move.y = 0;

        characterController.Move(move.normalized * moveSpeed * speedMultiplier * Time.deltaTime);
    }


    public Vector2 GetInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    public void OnAttackEnd()
    {
        stateMachine.ChangeState(new PlayerIdleState(this));
    }
}
