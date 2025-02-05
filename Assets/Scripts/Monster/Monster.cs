using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    EEnemyState currentState = EEnemyState.IDLE;
    public Transform target;    //현재 타겟
    public float detectionRange = 7f; // 탐지 범위
    public LayerMask targetLayer;   //타겟이 위치한 레이어

    public float moveSpeed = 2f;    //몬스터의 이동속도
    public float wanderRange = 15f; // 랜덤 이동 범위
    public float minIdleTime = 3f; // 최소 대기 시간
    public float maxIdleTime = 10f; // 최대 대기 시간

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        StartCoroutine(UpdateState());
    }

    protected override IEnumerator UpdateState()
    {
        while (true)
        {
            switch (currentState)
            {
                case EEnemyState.IDLE:
                    Idle();
                    break;
                case EEnemyState.MOVE:
                    Move();
                    break;
                case EEnemyState.ATTACK:
                    Attack();
                    break;
                case EEnemyState.DIE:
                    break;
            }
            yield return null;
        }
    }

    private void ChangeState(EEnemyState _currentstate)
    {
        currentState = _currentstate;
    }

    protected override void Idle()
    {
        SetAnimationState("animationState", (int)EEnemyState.IDLE);
        //StartCoroutine(RandomMove());
        SearchTarget();
    }

    public IEnumerator RandomMove()
    {
        Debug.Log("기다리는중");
        SearchTarget();
        float waitTime = Random.Range(minIdleTime, maxIdleTime);
        Debug.Log(waitTime);
        yield return WaitForSeconds(waitTime); // 대기 시간

        // 랜덤 위치로 이동
        SetAnimationState("animationState", (int)EEnemyState.MOVE);
        Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
        randomDirection += transform.position; // 현재 위치에 랜덤 방향 추가
        randomDirection.y = transform.position.y; // Y축 높이는 현재 위치와 동일하게 유지

        // 목표 위치로의 부드러운 이동
        Vector3 randomPosition = new Vector3(randomDirection.x, randomDirection.y, randomDirection.z);
        while (Vector3.Distance(transform.position, randomPosition) > 0.1f)
        {
            // 현재 위치에서 목표 위치로의 방향 계산
            Vector3 direction = (randomPosition - transform.position).normalized;

            // 타겟 방향으로 회전
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

            // 타겟 방향으로 이동
            transform.position = Vector3.MoveTowards(transform.position, randomPosition, moveSpeed * Time.deltaTime);
            SearchTarget();
            yield return null; // 다음 프레임까지 대기
        }

        Idle();
        Debug.Log("랜덤 위치로 이동 완료.");

    }

    public IEnumerator WaitForSeconds(float _seconds)
    {
        WaitForSeconds WaitForSeconds;
        WaitForSeconds = new WaitForSeconds(_seconds);

        yield return WaitForSeconds;
    }

    protected override void Move()
    {
        if (target != null)
        {
            SetAnimationState("animationState", (int)EEnemyState.MOVE);

            // 타겟까지의 방향 계산
            Vector3 direction = (target.position - transform.position).normalized;

            // 타겟 방향으로 이동
            transform.position += direction * moveSpeed * Time.deltaTime;

            // 타겟을 바라보도록 회전
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
        else
        {
            ChangeState(EEnemyState.IDLE);
        }

        SearchTarget();
    }

    protected override void Attack()
    {
        SetAnimationState("animationState", (int)EEnemyState.ATTACK);
    }

    public void SearchTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, targetLayer);

        if (hitColliders.Length > 0)
        {
            Debug.Log("타겟이 범위 내에 있습니다!");
            foreach (var collider in hitColliders)
            {
                //타겟 대상을 설정
                target = hitColliders[0].transform;
                ChangeState(EEnemyState.MOVE);
                Debug.Log($"타겟을 발견했습니다: {target.name}");
            }
        }
        else
        {
            Debug.Log("범위 내에 타겟이 없습니다.");
            ChangeState(EEnemyState.IDLE);
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 탐지 범위를 시각적으로 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    protected override void OnCollisionEnter(Collision collision)
    {

    }

    protected override void OnCollisionExit(Collision collision)
    {

    }
}
