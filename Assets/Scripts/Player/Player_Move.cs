using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class Player
{
    public float rotationSpeed = 1080f; //회전 속도

    protected override void Idle()
    {
        SetAnimationState("animationState", 0);
        SetAnimationMove("moveSpeed", 0);
    }

    protected override void Move()
    {
        // 이동 방향 초기화
        moveDirection = Vector3.zero;

        //회전 처리
        //RotateWithMouse();

        // W, A, S, D 키 입력 처리
        if (Input.GetKey(KeyCode.W))
        {
            // 정면으로 바라보게 회전
            Quaternion toRotationForward = Quaternion.Euler(0, 0, 0); // 정면으로 회전
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotationForward, rotationSpeed * Time.deltaTime);
            moveDirection += Vector3.forward; // 앞으로 이동
        }
        if (Input.GetKey(KeyCode.S))
        {
            // 뒤로 바라보게 회전
            Quaternion toRotationBackward = Quaternion.Euler(0, 180, 0); // 뒤로 회전
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotationBackward, rotationSpeed * Time.deltaTime);
            moveDirection += Vector3.back; // 뒤로 이동
        }
        if (Input.GetKey(KeyCode.A))
        {
            // 왼쪽으로 회전
            Quaternion toRotationLeft = Quaternion.Euler(0, -90, 0); // 왼쪽으로 90도 회전
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotationLeft, rotationSpeed * Time.deltaTime);
            moveDirection += Vector3.left; // 왼쪽으로 이동
        }
        if (Input.GetKey(KeyCode.D))
        {
            // 오른쪽으로 회전
            Quaternion toRotationRight = Quaternion.Euler(0, 90, 0); // 오른쪽으로 90도 회전
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotationRight, rotationSpeed * Time.deltaTime);
            moveDirection += Vector3.right; // 오른쪽으로 이동
        }

        // 입력 방향을 캐릭터의 로컬 방향으로 변환
        //moveDirection = transform.TransformDirection(moveDirection);

        //// 이동 방향 정규화
        //moveDirection = moveDirection.normalized;

        //// 이동 방향을 기반으로 캐릭터 회전
        //if (moveDirection != Vector3.zero)
        //{
        //    Quaternion toRotation = Quaternion.LookRotation(moveDirection);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360 * Time.deltaTime);
        //}

        //shift키를 안누르면 최대 0.5, shift키를 누르면 초대 1까지 값이 바뀌게 된다
        float offset = 0.5f + Input.GetAxis("Sprint") * 0.5f;

        // moveParameter 값에 따라 애니메이션 재생 (0: 대기, 0.5: 걷기, 1: 뛰기)
        SetAnimationMove("moveSpeed", offset);

        // moveDirection.x 값에 따라 애니메이션 재생 (-1: 왼쪽, 0: 가운데, 1: 오른쪽)
        SetAnimationMove("inputX", moveDirection.x);
        // moveDirection.z 값에 따라 애니메이션 재생 (-1: 뒤, 0: 가운데, 1: 앞)
        SetAnimationMove("inputZ", moveDirection.z);

        float moveSpeed = Mathf.Lerp(walkSpeed, runSpeed, Input.GetAxis("Sprint"));

        // Rigidbody.velocity를 사용하여 이동
        RIGIDBODY.velocity = new Vector3(moveDirection.x * moveSpeed, RIGIDBODY.velocity.y, moveDirection.z * moveSpeed);

        if (moveDirection == Vector3.zero)
        {
            ChangeState(EPlayerState.IDLE);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //ChangeState(EPlayerState.JUMP);
            SetAnimationState("animationState", (int)EPlayerState.JUMP);
            RIGIDBODY.velocity = new Vector3(RIGIDBODY.velocity.x, jumpPower, RIGIDBODY.velocity.z);
            isGrounded = false;
            Debug.Log("jump");
        }
    }
}
