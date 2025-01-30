using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public float jumpPower = 15.0f; //점프 강도
    public float slidingPower = 10.0f;  // 대쉬 속도
    public float pastDirection = 1.0f;  //마지막 방향
    public float moveSpeed = 5f;    //이동 속도
    public Vector3 moveDirection;  //현재 방향
    public float horizontal; //x축 방향
    public float vertical;  //z축 방향
}

