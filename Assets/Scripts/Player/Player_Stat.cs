using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public float health = 100.0f; //현재 Hp
    public float jumpPower = 7.0f; //점프 강도
    public float slidingPower = 10.0f;  // 대쉬 속도
    public float walkSpeed = 4.0f;    //걷기 속도
    public float runSpeed = 8.0f;  //뛰기 속도
    public float giveDamage = 10f; //현재 공격력
    public Vector3 moveDirection;  //현재 방향
}

