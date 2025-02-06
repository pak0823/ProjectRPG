using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{
    public EPlayerState currentState;
    public bool isGrounded;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        //StartCoroutine(UpdateState());
    }

    //protected override IEnumerator UpdateState()
    //{
    //    //while (true)
    //    //{
    //    //    InputManager();

    //    //    switch (currentState)
    //    //    {
    //    //        case EPlayerState.IDLE:
    //    //            Idle();
    //    //            break;
    //    //        case EPlayerState.ATTACK:
    //    //            Attack();
    //    //            break;
    //    //    }
    //    //    yield return null;
    //    //}

    //    yield return null;
    //}

    private void Update()
    {
        InputManager();

        if (currentState == EPlayerState.IDLE)
            Idle();
    }

    void FixedUpdate()
    {
        if (currentState == EPlayerState.MOVE)
        {
            Move();
        }
    }

    private void ChangeState(EPlayerState _currentstate)
    {
        currentState = _currentstate;
    }

    private void InputManager()
    {
        //키 입력 확인
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))   //이동
        {
            if(currentState != EPlayerState.ATTACK && currentState != EPlayerState.DEFEND) // 공격상태 또는 방어 상태가 아닐 때만 이동 가능
                ChangeState(EPlayerState.MOVE);
        }
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭 시 공격
        {
            ChangeState(EPlayerState.ATTACK);
            Attack();
        }
        if (Input.GetMouseButton(1)) //마우스 오른쪽 클릭 시 방어
        {
            Defend();

            if (Input.GetMouseButton(0)) // 마우스 왼쪽 클릭 시 공격
            {
                ChangeState(EPlayerState.ATTACK);
                Attack();
            }
        }
        if (Input.GetMouseButtonUp(1)) // 마우스 오른쪽 버튼에서 손을 뗐을 때
        {
            ChangeState(EPlayerState.IDLE); // Idle 상태로 변경
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            ChangeState(EPlayerState.IDLE);
        }
    }

    public override void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
