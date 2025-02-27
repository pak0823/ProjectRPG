using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public float rotationSpeed = 1080f; //회전 속도
    public bool usingStamina = false;

    public float decreaseEndTime = 0f; //스태미너 감소가 끝난 시간

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

        if (Input.GetMouseButton(0) && CanAttack())
        {
            moveDirection = Vector3.zero; // 이동 방향 초기화
            RIGIDBODY.velocity = Vector3.zero; // Rigidbody 속도 초기화
            ChangeState(EPlayerState.ATTACK);
            return;
        }
        else
        {
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

            // Rigidbody에 힘을 추가하여 이동
            Vector3 velocity = desiredDirection * currentSpeed;
            velocity.y = RIGIDBODY.velocity.y; // 현재 Y축 속도 유지
            RIGIDBODY.velocity = velocity; // 새로운 속도 설정
        }

        if (moveDirection == Vector3.zero)
        {
            if(currentState != EPlayerState.ATTACK && currentState != EPlayerState.SKILL)
                ChangeState(EPlayerState.IDLE);
        }
    }

    //스태미너 증가 함수
    private void IncreaseStamina()
    {
        if (currentStamina <= maxStamina && !usingStamina)
        {
            currentStamina += 0.05f;
            Shared.staminaBar.Stamina(currentStamina);
        }
    }
    //스태미너 감소 함수
    private void DecreaseStamina()
    {
        if (currentStamina >= 0)
        {
            usingStamina = true;
            currentStamina -= 0.5f;
            decreaseEndTime = Time.time;
            Shared.staminaBar.Stamina(currentStamina);
        }
        else
            Debug.Log("CurrentStamina is not enough!");
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //ChangeState(EPlayerState.JUMP);
            //SetAnimationState("animationState", (int)EPlayerState.JUMP);
            RIGIDBODY.velocity = new Vector3(RIGIDBODY.velocity.x, jumpPower, RIGIDBODY.velocity.z);
            isGrounded = false;
        }
    }

    public override void TakeDamage(float _damage)
    {
        if (CanHit())
        {
            if (currentState == EPlayerState.DEFEND)
            {
                ChangeState(EPlayerState.DEFENDHIT);
                SetAnimationState("animationState", (int)EPlayerState.DEFENDHIT);
                currentHealth -= (_damage * 0.8f);
                Debug.Log("방패를 들고 피해입음");
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

            Shared.hpBar.HealthBar(currentHealth);
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
