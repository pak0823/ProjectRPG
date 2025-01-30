using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{
    public EPlayerState currentState;

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
                    Move();
                    break;
                case EPlayerState.ATTACK:
                    Attack();
                    break;
            }
            yield return null;
        }
    }

    public void SetAnimationState(string _name, int _num)
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

    public void ChangeState(EPlayerState _currentstate)
    {
        currentState = _currentstate;
    }

    protected override void Idle()
    {
        SetAnimationState("animationState", (int)EPlayerState.IDLE);

        if (horizontal != 0 || vertical != 0)
            ChangeState(EPlayerState.MOVE);
    }

    protected override void Move()
    {
        SetAnimationState("animationState", (int)EPlayerState.MOVE);

        // 이동 방향 설정
        moveDirection = new Vector3(horizontal, 0, vertical).normalized; // 방향 벡터 정규화

        // 이동 방향을 기반으로 캐릭터 회전
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }

        RIGIDBODY.MovePosition(RIGIDBODY.position + moveDirection * moveSpeed * Time.deltaTime);

        if (horizontal == 0 && vertical == 0)
            ChangeState(EPlayerState.IDLE);
    }
    //private void Jump()
    //{
    //    RIGIDBODY.velocity = Vector2.up * jumpPower;
    //}

    //private void Sliding()
    //{
    //    currentState = EPlayerState.SLIDING;
    //    RIGIDBODY.velocity = Vector2.right * slidingPower * pastDirection;
    //    SetAnimationState("animationSatae", (int)EPlayerState.SLIDING);
    //}

    protected override void Attack()
    {
        SetAnimationState("animationState", (int)EPlayerState.ATTACK);

        if (Input.GetKeyUp(KeyCode.A))
        {
            //currentWeapon.NormalAttack();
        }
        else
            return;
    }

    private void InputManager()
    {
        // 입력을 받아 이동 방향 계산
        horizontal = Input.GetAxis("Horizontal"); // A, D, Left Arrow, Right Arrow
        vertical = Input.GetAxis("Vertical"); // W, S, Up Arrow, Down Arrow
    }
}
