using System.Collections;
using UnityEngine;

public partial class Monster : Character
{
    public float health = 100f; // 몬스터의 체력
    public float moveSpeed = 15f; // 몬스터의 이동 속도
    public float detectionRange = 7f; // 탐지 범위
    public LayerMask targetLayer; // 타겟이 위치한 레이어

    protected Transform target; // 현재 타겟
    protected Animator animator;
    protected Rigidbody rigidBody;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>(); // Animator 초기화
        rigidBody = GetComponent<Rigidbody>(); // Rigidbody 초기화
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    public Transform GetTarget()
    {
        return target;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        SetAnimationState("animationState", (int)EEnemyState.DIE);
        // 죽음 로직 추가
    }

    public override void OnCollisionEnter(Collision collision)
    {
        // 충돌 처리 로직
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
