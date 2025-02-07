using System.Collections;
using UnityEngine;

public class AiMonster : MonoBehaviour
{
    private Monster monster; // Monster 클래스의 인스턴스
    private EEnemyState currentState = EEnemyState.IDLE;

    protected virtual void Awake()
    {
        monster = GetComponent<Monster>();
    }

    protected virtual void Start()
    {
        StartCoroutine(UpdateState());
    }

    protected IEnumerator UpdateState()
    {
        while (true)
        {
            switch (currentState)
            {
                case EEnemyState.IDLE:
                    monster.Idle();
                    SearchTarget();
                    break;
                case EEnemyState.MOVE:
                    Move();
                    SearchTarget();
                    break;
                case EEnemyState.ATTACK:
                    Attack();
                    break;
                case EEnemyState.DIE:
                    Die();
                    break;
            }
            yield return null;
        }
    }

    public void ChangeState(EEnemyState newState)
    {
        currentState = newState;
    }

    protected void Move()
    {
        if (monster.GetTarget() != null)
        {
            monster.Move();

            if (monster.distanceToTarget <= monster.attackRange)
            {
                ChangeState(EEnemyState.ATTACK);
            }
        }
        else
        {
            ChangeState(EEnemyState.IDLE);
        }
    }

    protected void Attack()
    {
        if (monster.GetTarget() != null)
        {
            monster.Attack();
        }
        else
        {
            ChangeState(EEnemyState.IDLE);
        }
    }

    protected void TakeDamage()
    {
        //monster.TakeDamage();
    }

    protected void Die()
    {
        monster.Die();
    }

    protected void SearchTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, monster.detectionRange, monster.targetLayer);
        if (hitColliders.Length > 0)
        {
            Transform target = hitColliders[0].transform; // 첫 번째 타겟을 설정
            monster.SetTarget(target);

            if(currentState == EEnemyState.IDLE)
                ChangeState(EEnemyState.MOVE);
        }
        else
        {
            monster.SetTarget(null); // 타겟을 null로 설정
            ChangeState(EEnemyState.IDLE);
        }
    }
}
