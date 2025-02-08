using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public override void Attack()
    {
        SetAnimationState("animationState", (int)EPlayerState.ATTACK);
        lastAttackTime = Time.time;

    }

    public void Defend()
    {
        currentState = EPlayerState.DEFEND;
        SetAnimationState("animationState", (int)EPlayerState.DEFEND);
    }
}
