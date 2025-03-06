using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    private float parryingTrueTime = 10f / 60f; //패링 가능한 시간
    private bool isParrying = false; // 패링 성공 유무
    private float monsterAttackTime; //몬스터의 마지막 공격 시간
    private bool isAttacking = false; // 공격 상태 변수
    private bool isSkill = false; // 스킬 상태 변수

    public override void Attack()
    {
        isAttacking = true;
        SetAnimationState("animationState", (int)EPlayerState.ATTACK);
        lastAttackTime = Time.time;
    }
    public void EndAttack()
    {
        isAttacking = false;
        Debug.Log("공격 종료");
    }

    public void StartSkill(int skillIndex)
    {
        // 스킬 사용 로직
        Shared.skillCoolDown.StartCooldown(skillIndex);
        isSkill = true;
    }
    public void EndSkill()
    {
        IsSkill = false; // 스킬 종료
        Debug.Log("스킬 종료");
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
                    StartSkill(0);
                }
                else if (Input.GetKeyDown(KeyCode.R) && !Shared.skillCoolDown.isCooldown[1])
                {
                    ChangeState(EPlayerState.SKILL);
                    SetAnimationState("animationState", 11);
                    StartSkill(1);
                }
            } 
        }
    }


    private void Defend()
    {
        Parrying();

        if (Input.GetMouseButton(1))
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

        
    }

    private void Parrying()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if ((Time.time <= monsterAttackTime + parryingTrueTime) && (Time.time >= monsterAttackTime - parryingTrueTime) ) //패링 가능시간 내에 패링을 했는지 확인
            {
                //패링에 성공했을 시 몬스터에게 똑같은 대미지를 넘겨줌
                Debug.Log("패링성공");
                isParrying = true;
            }
        }
    }

    public bool CanAttack()
    {
        // 쿨타임이 지난 경우에만 true 반환
        return Time.time >= lastAttackTime + attackCoolDown;
    }

    public float OnAttackDetected { set { monsterAttackTime = value; } get { return monsterAttackTime; } }// 적의 공격 감지 시 호출
    public bool IsParrying { set { isParrying = value; } get { return isParrying; } }
    public bool IsAttacking { set { isAttacking = value; } get { return isAttacking; } }
    public bool IsSkill { set { isSkill = value; } get { return isSkill; } }
}
