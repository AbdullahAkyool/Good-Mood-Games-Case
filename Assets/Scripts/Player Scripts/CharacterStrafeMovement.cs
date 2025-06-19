using UnityEngine;

public class CharacterStrafeMovement : MonoBehaviour
{
    private Animator animator;
    private Vector2 strafeDirection;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        strafeDirection.x = Input.GetAxis("Horizontal");
        strafeDirection.y = Input.GetAxis("Vertical");

        animator.SetFloat("InputX", strafeDirection.x);
        animator.SetFloat("InputY", strafeDirection.y);
    }
}
