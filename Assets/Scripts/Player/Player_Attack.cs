using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public bool isParrying = false;
    public override void Attack()
    {
        SetAnimationState("animationState", (int)EPlayerState.ATTACK);
        lastAttackTime = Time.time;

    }

    public void Defend()
    {
        SetAnimationState("animationState", (int)EPlayerState.DEFEND);
        if(Input.GetMouseButtonDown(1))
        {
            if (monsterAttackTime > 0)
            {
                if (Time.time < monsterAttackTime + attackWindowTime) //패링 가능시간 내에 패링을 했는지 확인
                {
                    //패링에 성공했을 시 몬스터에게 똑같은 대미지를 넘겨줌
                    isParrying = true;
                    Debug.Log("패링성공");
                }
                else
                {
                    isParrying = false;
                }
            }
        }
    }
}
