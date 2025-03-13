using System.Collections;
using UnityEngine;

//상태 기반 행동 조합 방식을 적용
public class AiMonster : MonoBehaviour
{
    private Monster monster; // Monster 클래스의 인스턴스
    [SerializeField]private EEnemyState currentState = EEnemyState.IDLE;
    public DynamicTextData textData;    //대미지 텍스트

    // 쿨타임 관련 변수
    private float attackCooldown = 1.5f; // 공격 쿨타임
    private float lastAttackTime = 0f; // 마지막 공격 시간
    private float invincibilityTime = 0.5f; //피격 후 무적시간
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
    public bool CanHit()
    {
        // 무적 상태가 아닐 경우 true 반환
        return Time.time >= lastHitTime + invincibilityTime;
    }

    protected void SearchTarget()
    {
        Collider[] hitColliderDetectionRange = Physics.OverlapSphere(transform.position, monster.detectionRange, monster.targetLayer);//접근 허용 범위
        Collider[] hitColliderViewRange = Physics.OverlapSphere(transform.position, monster.viewRange, monster.targetLayer);//시야 허용 범위

        foreach (var hitCollider in hitColliderViewRange)
        {
            Player target = hitCollider.GetComponent<Player>();

            if (target != null)
            {
                if (IsInView(target.transform)) //타겟이 시야범위에 들어왔을 시 추적
                {
                    monster.SetTarget(target.transform, target);
                    if (currentState == EEnemyState.IDLE || currentState == EEnemyState.MOVE)
                    {
                        ChangeState(EEnemyState.MOVE); // 추적 시작
                    }
                    return; // 타겟을 찾으면 종료
                }

                foreach (var hitColliders in hitColliderDetectionRange)
                {
                    if (hitColliders != null)   //타겟이 접근 허용범위에 초과했을 경우 시야와 상관없이 추적
                    {
                        //Debug.Log(hitColliders.name);
                        monster.SetTarget(target.transform, target);
                        if (currentState == EEnemyState.IDLE || currentState == EEnemyState.MOVE)
                        {
                            ChangeState(EEnemyState.MOVE); // 추적 시작
                        }
                    }
                    return; // 타겟을 찾으면 종료
                }
            }
        }

        // 타겟이 범위 밖으로 나가면 IDLE 상태로 변경
        if (hitColliderViewRange.Length <= 0)
        {
            monster.SetTarget(null, null); // 타겟을 null로 설정
            ChangeState(EEnemyState.IDLE);
            //Debug.Log("타겟이 범위 밖으로 나감");
        }
    }

    private bool IsInView(Transform target)
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToTarget);


        if (angle < monster.viewAngle / 2 && Vector3.Distance(transform.position, target.position) <= monster.viewRange)
        {
            int layerMask = ~LayerMask.GetMask("Default"); // "Default" 레이어를 제외

            RaycastHit hit; // RaycastHit 구조체 선언

            // 장애물 체크
            if (!Physics.Linecast(transform.position, target.position, out hit, layerMask))
            {
                //Debug.Log("타겟이 시야범위 안으로 들어옴");
                return true;
            }
            else
            {
                //Debug.Log("타겟과 몬스터 사이에 장애물이 있습니다: " + hit.collider.gameObject.name);
            }
        }
        else 
        {
                //Debug.Log("타겟이 시야범위 밖으로 나감");
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (monster == null)
        {
            monster = GetComponent<Monster>();
            if (monster == null)
            {
                Debug.LogWarning("Monster 컴포넌트가 없습니다.");
                return; // Monster가 없으면 메서드 종료
            }
        }

        // 몬스터의 시야 범위를 시각적으로 표시
        Gizmos.color = Color.yellow; // 색상 설정
        Vector3 forward = transform.forward * monster.viewRange;

        // 시야 범위 원
        Gizmos.DrawWireSphere(transform.position, monster.viewRange);

        // 시야 각도 표시
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, monster.viewAngle / 2, 0) * forward);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, -monster.viewAngle / 2, 0) * forward);

        // 시야 방향 표시
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + forward);

        // 일정 범위 접근 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, monster.detectionRange);
    }
}
