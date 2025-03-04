using System.Collections;
using UnityEngine;

//상태 기반 행동 조합 방식을 적용
public class AiMonster : MonoBehaviour
{
    private Monster monster; // Monster 클래스의 인스턴스
    [SerializeField]private EEnemyState currentState = EEnemyState.IDLE;
    public DynamicTextData textData;    //대미지 텍스트

    // 쿨타임 관련 변수
    public float attackCooldown = 1.0f; // 공격 쿨타임
    private float lastAttackTime = 0f; // 마지막 공격 시간
    public float invincibilityTime = 1.0f; //피격 후 무적시간
    private float lastHitTime = 0f; // 마지막 피격 시간

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
                    HandleIdleState();
                    break;
                case EEnemyState.MOVE:
                    HandleMoveState();
                    SearchTarget();
                    break;
                case EEnemyState.ATTACK:
                    HandleAttackState();
                    break;
                case EEnemyState.DIE:
                    Die();
                    break;
            }
            yield return null;
        }
    }

    public void ChangeState(EEnemyState _newState)
    {
        currentState = _newState;
    }

    private void HandleIdleState()
    {
        monster.Idle();
        SearchTarget();
    }

    protected void HandleMoveState()
    {
        if (monster.GetTarget() != null)
        {
            if(currentState != EEnemyState.HIT)
            {
                monster.Move();

                if (monster.distanceToTarget <= monster.attackRange)
                {
                    ChangeState(EEnemyState.ATTACK);
                }
            }
        }
        else
        {
            ChangeState(EEnemyState.IDLE);
        }
    }

    protected void HandleAttackState()
    {
        if (monster.GetTarget() != null)
        {
            if (currentState != EEnemyState.HIT)
            {
                if (CanAttack())
                {
                    monster.Attack();
                    lastAttackTime = Time.time;
                }
            }
        }
        else
        {
            ChangeState(EEnemyState.IDLE);
        }
    }

    public void TakeDamage(float _damage)
    {
        ChangeState(EEnemyState.HIT);
        monster.TakeDamage(_damage);
        lastHitTime = Time.time; // 마지막 피격 시간 업데이트

        if (monster.health <= 0)
        {
            ChangeState(EEnemyState.DIE);
        }
    }

    protected void Die()
    {
        monster.Die();
    }

    private bool CanAttack()
    {
        // 쿨타임이 지난 경우에만 true 반환
        return Time.time >= lastAttackTime + attackCooldown;
    }
    private bool CanHit()
    {
        // 무적 상태가 아닐 경우 true 반환
        return Time.time >= lastHitTime + invincibilityTime;
    }

    protected void SearchTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, monster.detectionRange, monster.targetLayer);

        if (hitColliders.Length > 0)
        {
            Transform targetPosition = hitColliders[0].transform; // 첫 번째 타겟을 설정
            Player target = targetPosition.GetComponent<Player>();
            monster.SetTarget(targetPosition, target);

            if(currentState == EEnemyState.IDLE)
                ChangeState(EEnemyState.MOVE);
        }
        else
        {
            monster.SetTarget(null,null); // 타겟을 null로 설정
            ChangeState(EEnemyState.IDLE);
        }
    }
}
