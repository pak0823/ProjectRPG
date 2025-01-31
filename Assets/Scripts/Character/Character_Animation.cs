using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Character
{
    public Animator ANIMATOR;

    public void SetAnimationState(string _name, int _num)   //현재 상태에 따른 애니메이션 변경 ex) 이동, 공격, 점프 등
    {
        if (ANIMATOR != null)
        {
            ANIMATOR.SetInteger(_name, _num);
        }
        else
        {
            Debug.LogError("Animator is not assigned! Current GameObject: " + gameObject.name);
        }
    }
    public void SetAnimationMove(string _name, float _value)    //현재 이동방향에 따른 애니메이션 변경 ex) 대기, 앞으로 이동, 뒤로 이동, 뛰기 등
    {
        if (ANIMATOR != null)
        {
            ANIMATOR.SetFloat(_name, _value);
        }
        else
        {
            Debug.LogError("Animator is not assigned! Current GameObject: " + gameObject.name);
        }
    }
}
