using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    protected override void Attack()
    {
        SetAnimationState("animationState", (int)EPlayerState.ATTACK);

    }

    public void Defend()
    {
        currentState = EPlayerState.DEFEND;
        SetAnimationState("animationState", (int)EPlayerState.DEFEND);
    }
}
