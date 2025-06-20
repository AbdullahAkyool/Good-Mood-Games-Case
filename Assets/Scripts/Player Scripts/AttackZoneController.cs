using System.Collections.Generic;
using UnityEngine;

public class AttackZoneController : MonoBehaviour
{
    private Collider attackZoneCollider;
    public Collider AttackZoneCollider => attackZoneCollider;

    [SerializeField] private List<EnemyHealthController> enemiesOnTarget;
    public List<EnemyHealthController> EnemiesOnTarget => enemiesOnTarget;

    private void Awake()
    {
        attackZoneCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyHealthController enemyHealthController) &&
            !enemiesOnTarget.Contains(enemyHealthController))
        {
            enemiesOnTarget.Add(enemyHealthController);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyHealthController enemyHealthController) &&
            enemiesOnTarget.Contains(enemyHealthController))
        {
            enemiesOnTarget.Remove(enemyHealthController);
        }
    }
}
