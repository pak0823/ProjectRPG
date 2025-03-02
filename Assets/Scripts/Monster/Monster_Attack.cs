using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster
{
    private float lastAttackTime; //몬스터의 마지막 공격 시간
    public override void Attack()
    {
        SetAnimationState("animationState", (int)EEnemyState.ATTACK);
        // 공격 로직 추가
    }

    public void OnAttackDetected()
    {
        if (target != null)
        {
            target.OnAttackDetected = Time.time;
            Debug.Log("공격시간 넘겨줌");
        }
        else
        {
            Debug.Log("target is null!");
            return;
        }
    }




    public float monsterAttackTime { set { lastAttackTime = value; } get { return lastAttackTime; } }
}
