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

    public float monsterAttackTime { set { lastAttackTime = value; } get { return lastAttackTime; } }
}
