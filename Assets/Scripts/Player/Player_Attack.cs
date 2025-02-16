using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    private bool isParrying = false;
    private float lastDecreaseTime;
    public override void Attack()
    {
        SetAnimationState("animationState", (int)EPlayerState.ATTACK);
        lastAttackTime = Time.time;
    }

    public void UseSkill(int skillIndex)
    {
        // 스킬 사용 로직
        Shared.skillCoolDown.StartCooldown(skillIndex);
    }


    private void Defend()
    {
        if(Input.GetMouseButton(1) && currentStamina > 0)
        {
            SetAnimationState("animationState", (int)EPlayerState.DEFEND);
            
            if (Time.time >= decreaseEndTime + 0.05f)
            {
                DecreaseStamina();
                decreaseEndTime = Time.time;
            }
            else
            {
                usingStamina = false;
            }

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

            if (currentStamina <= 0)
            {
                ChangeState(EPlayerState.IDLE);
            }
        }
    }

    public bool IsParrying { get { return isParrying; } }
}
