using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public float rotationSpeed = 1080f; //회전 속도
    public bool usingStamina = false;

    public float runEndTime = 0f; //달리기 끝난 시간

    public override void Idle()
    {
        SetAnimationState("animationState", (int)EPlayerState.IDLE);
        SetAnimationMove("moveSpeed", 0);
    }

    public override void Move()
    {
        
        SetAnimationState("animationState", 0);
        SetAnimationMove("moveSpeed", currentSpeed);

        // 이동 방향 초기화
        moveDirection = Vector3.zero;

        // W, A, S, D 키 입력 처리
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector3.forward; // 앞으로 이동
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector3.back; // 뒤로 이동
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector3.left; // 왼쪽으로 이동
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector3.right; // 오른쪽으로 이동
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
        if(Input.GetMouseButton(0))
        {
            ChangeState(EPlayerState.ATTACK);
        }
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)    //달리면서 이동
        {
            currentSpeed = runSpeed;
            DecreaseStamina();
        }
        else
        {
            usingStamina = false;
            currentSpeed = walkSpeed;
        }

        // 카메라의 방향을 가져오기
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0; // Y축 회전을 무시
        cameraForward.Normalize();

        // 이동 방향을 카메라 방향에 맞추기
        if (moveDirection != Vector3.zero)
        {
            Vector3 desiredDirection = cameraForward * moveDirection.z + Camera.main.transform.right * moveDirection.x;

            // 캐릭터 회전
            Quaternion toRotation = Quaternion.LookRotation(desiredDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            // Rigidbody.velocity를 사용하여 이동
            RIGIDBODY.velocity = desiredDirection * currentSpeed;
        }

        if (moveDirection == Vector3.zero)
        {
            ChangeState(EPlayerState.IDLE);
        }
    }

    //스태미너 증가 함수
    private void IncreaseStamina()
    {
        if (currentStamina <= maxStamina && !usingStamina)
        {
            currentStamina += 1f;
            Shared.Ui_Ingame.StaminaBar();
        }
    }
    //스태미너 감소 함수
    private void DecreaseStamina()
    {
        if (currentStamina >= 0)
        {
            usingStamina = true;
            currentStamina -= 1f;
            runEndTime = Time.time;
            Shared.Ui_Ingame.StaminaBar();
        }
    }

    private void Jump()
    {
        //현재 이동하면서 점프가 안됨 수정 필요
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(EPlayerState.JUMP);
            SetAnimationState("animationState", (int)EPlayerState.JUMP);
            RIGIDBODY.velocity = new Vector3(RIGIDBODY.velocity.x, jumpPower, RIGIDBODY.velocity.z);
            isGrounded = false;
        }
    }

    public override void TakeDamage(float _damage)
    {
        if (CanHit())
        {
            if(currentState == EPlayerState.DEFEND)
            {
                currentHealth -= (_damage * 0.8f);
                ChangeState(EPlayerState.DEFENDHIT);
                SetAnimationState("animationState", (int)EPlayerState.DEFENDHIT);
            }
            else
            {
                ChangeState(EPlayerState.HIT);
                SetAnimationState("animationState", (int)EPlayerState.HIT);
                currentHealth -= _damage;
            }

            if (currentHealth <= 0)
            {
                Die();
            }

            Shared.Ui_Ingame.HealthBar(currentHealth);
        }
        else
            return;

        Debug.Log($"남은 PlayerHP:{currentHealth}");
    }

    public void Die()
    {
        ChangeState(EPlayerState.DIE);
        SetAnimationState("animationState", (int)EPlayerState.DIE);
        //Die애니메이션에 DestroyObject() 이벤트 추가
    }
}
