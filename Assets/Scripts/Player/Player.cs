using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        //while (true)
        //{
        //    InputManager();

        //    switch (currentState)
        //    {
        //        case EPlayerState.IDLE:
        //            Idle();
        //            break;
        //        case EPlayerState.ATTACK:
        //            Attack();
        //            break;
        //    }
        //    yield return null;
        //}

        yield return null;
    }

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
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭 시 공격
        {
            ChangeState(EPlayerState.ATTACK);
            Attack();
        }
        if(Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("ground");
        }
    }

    protected override void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("No ground");
        }
    }
}
