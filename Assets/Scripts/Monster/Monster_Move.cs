using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public partial class Monster
{
    public float distanceToTarget; // 타겟과의 거리
    public override void Idle()
    {
        SetAnimationState("animationState", (int)EEnemyState.IDLE);
    }

    public override void Move()
    {
        if (target != null)
        {
            SetAnimationState("animationState", (int)EEnemyState.MOVE);
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0; // Y축 방향 고려

            // 타겟과의 거리 계산
            distanceToTarget = Vector3.Distance(transform.position, target.position);

            // 타겟 방향으로 이동
            rigidBody.MovePosition(Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed));

            // 타겟을 바라보도록 회전
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }

}
