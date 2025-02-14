using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    private float maxHealth = 100.0f;    //최대 Hp
    private float currentHealth = 100.0f; //현재 Hp
    public float maxStamina = 100.0f;   //최대 Stamina
    public float currentStamina = 100.0f;   //현재 Stamina
    public float jumpPower = 5.0f; //점프 강도
    public float slidingPower = 10.0f;  // 대쉬 속도
    public float walkSpeed = 4.0f;    //걷기 속도
    public float runSpeed = 8.0f;  //뛰기 속도
    public float currentSpeed = 0f; //현재 속도
    public float giveDamage = 10f; //현재 공격력
    public Vector3 moveDirection;  //현재 방향


    public float GetMaxHp()
    {
        return maxHealth;
    }
    public float GetMaxStamina()
    {
        return maxStamina;
    }
    public float GetCurrentHp()
    {
        return currentHealth;
    }
}

