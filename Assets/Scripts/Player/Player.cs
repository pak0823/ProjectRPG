using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        StartCoroutine(UpdateState());
    }

    protected override IEnumerator UpdateState()
    {
        while (true)
        {
            InputManager();

            switch (currentState)
            {
                case EPlayerState.IDLE:
                    Idle();
                    break;
                case EPlayerState.MOVE:
                    //Move();
                    break;
                case EPlayerState.ATTACK:
                    Attack();
                    break;
            }
            yield return null;
        }
    }

    void FixedUpdate()
    {
        if (currentState == EPlayerState.MOVE)
        {
            Move();
        }
    }

    public void ChangeState(EPlayerState _currentstate)
    {
        currentState = _currentstate;
    }

    private void InputManager()
    {
        //키 입력 확인
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))   //이동
        {
            ChangeState(EPlayerState.MOVE);
        }
        else if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭 시 공격
        {
            ChangeState(EPlayerState.ATTACK);
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
        else //아무 입력이 없을 시 idle상태로 변경
        {
            ChangeState(EPlayerState.IDLE);
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    protected override void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
