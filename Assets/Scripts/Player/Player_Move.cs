using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public float rotationSpeed = 1080f; //회전 속도

    public override void Idle()
    {
        SetAnimationState("animationState", 0);
        SetAnimationMove("moveSpeed", 0);
    }

    public override void Move()
    {
        SetAnimationState("animationState", 0);

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
            // 오른쪽으로 회전
            moveDirection += Vector3.right; // 오른쪽으로 이동
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
            float moveSpeed = Mathf.Lerp(walkSpeed, runSpeed, Input.GetAxis("Sprint"));
            RIGIDBODY.velocity = desiredDirection * moveSpeed;
        }

        //shift키를 안누르면 최대 0.5, shift키를 누르면 초대 1까지 값이 바뀌게 된다
        float offset = 0.5f + Input.GetAxis("Sprint") * 0.5f;

        // moveParameter 값에 따라 애니메이션 재생 (0: 대기, 0.5: 걷기, 1: 뛰기)
        SetAnimationMove("moveSpeed", offset);

        if (moveDirection == Vector3.zero)
        {
            ChangeState(EPlayerState.IDLE);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            ChangeState(EPlayerState.JUMP);
            SetAnimationState("animationState", (int)EPlayerState.JUMP);
            RIGIDBODY.velocity = new Vector3(RIGIDBODY.velocity.x, jumpPower, RIGIDBODY.velocity.z);
            isGrounded = false;
        }
    }
}
