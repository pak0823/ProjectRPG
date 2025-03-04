using System.Collections;
using UnityEngine;

public partial class Monster : Character
{
    public float health = 100f; // 몬스터의 체력
    public float moveSpeed = 15f; // 몬스터의 이동 속도
    public float detectionRange = 7f; // 탐지 범위
    public float attackRange = 1.4f;   //공격 가능 거리
    public float giveDamage = 5f; //현재 공격력
    public LayerMask targetLayer; // 타겟이 위치한 레이어

    public Transform targetPosition; // 현재 타겟의 위치
    private Animator animator;
    private Rigidbody rigidBody;
    public AiMonster aiMonster; //AiMonster 클래스의 인스턴스
    public Player target;




    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>(); // Animator 초기화
        rigidBody = GetComponent<Rigidbody>(); // Rigidbody 초기화
        aiMonster = GetComponent<AiMonster>();
    }

    public void SetTarget(Transform _targetPos, Player _target)
    {
        targetPosition = _targetPos;
        target = _target;
    }
    public Transform GetTarget()
    {
        return targetPosition;
    }

    public override void TakeDamage(float _damage)
    {
        health -= _damage;
        SetAnimationState("animationState", (int)EEnemyState.HIT);

        //Debug.Log($"남은 MonsterHP:{health}");
    }

    public void Die()
    {
        SetAnimationState("animationState", (int)EEnemyState.DIE);
        //Die애니메이션에 DestroyObject() 이벤트 추가
        // 죽음 로직 추가
    }

    protected override IEnumerator DestroyObject(float _destroytime)
    {
        CAPSULECOLLIDER.enabled = false;
        yield return new WaitForSeconds(_destroytime);
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject.GetComponent<Player>();

            if (target != null)
            {
                if (!target.IsParrying)
                    target.TakeDamage(giveDamage);
                else
                {
                    Debug.Log("몬스터가 패링대미지 입음");
                    TakeDamage(giveDamage);
                    target.IsParrying = false;
                }
                    
            }
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

        }
    }

    public override void OnCollisionExit(Collision collision)
    {
        // 충돌 종료 처리 로직
        if (collision.gameObject.CompareTag("Ground"))
        {

        }
    }
}
