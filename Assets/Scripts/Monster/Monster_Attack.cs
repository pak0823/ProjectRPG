using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster
{
    public override void Attack()
    {
        SetAnimationState("animationState", (int)EEnemyState.ATTACK);
        target.OnAttackDetected();
        // 공격 로직 추가
    }
}
