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

    public void UseSkill(int skillIndex)
    {
        // 스킬 사용 로직
        Shared.skillCoolDown.StartCooldown(skillIndex);
    }

    private void HandleSkillInput()
    {
        if(currentState == EPlayerState.IDLE || currentState == EPlayerState.MOVE)
        {
            if(currentState != EPlayerState.SKILL)
            {
                if (Input.GetKeyDown(KeyCode.E) && !Shared.skillCoolDown.isCooldown[0])
                {
                    ChangeState(EPlayerState.SKILL);
                    SetAnimationState("animationState", 10);
                    UseSkill(0);
                }
                else if (Input.GetKeyDown(KeyCode.R) && !Shared.skillCoolDown.isCooldown[1])
                {
                    ChangeState(EPlayerState.SKILL);
                    SetAnimationState("animationState", 11);
                    UseSkill(1);
                }
            } 
        }
    }


    private void Defend()
    {
        if(Input.GetMouseButton(1))
        {
            if (currentStamina > 0)
            {
                SetAnimationState("animationState", (int)EPlayerState.DEFEND);

                if (Time.time >= decreaseEndTime + 0.05f)//스태미너 감소하는 크기 조절
                {
                    DecreaseStamina();
                    decreaseEndTime = Time.time;
                }
                else
                {
                    usingStamina = false;
                }
            }
            else
                ChangeState(EPlayerState.IDLE);
        }

        //if(Input.GetMouseButtonDown(1))
        //{
        //    if (monsterAttackTime > 0)
        //    {
        //        if (Time.time < monsterAttackTime + parryingTrueTime) //패링 가능시간 내에 패링을 했는지 확인
        //        {
        //            //패링에 성공했을 시 몬스터에게 똑같은 대미지를 넘겨줌
        //            isParrying = true;
        //            Debug.Log("패링성공");
        //        }
        //    }
        //}
    }

    
}
