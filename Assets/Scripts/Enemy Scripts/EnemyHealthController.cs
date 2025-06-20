using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    private EnemyController enemyController;

    public int MaxHealth = 100;
    public int CurrentHealth = 100;

    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;

            enemyController.stateMachine.ChangeState(new EnemyDieState(enemyController));
        }
    }
}
