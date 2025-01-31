using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class Player
{
    public float rotationSpeed = 270f; //회전 속도

    protected override void Idle()
    {
        SetAnimationMove("moveSpeed", 0);
        RotateWithMouse();
    }

    protected override void Move()
    {
        // 이동 방향 초기화
        moveDirection = Vector3.zero;

        // W, A, S, D 키 입력 처리
        if (Input.GetKey(KeyCode.W)) moveDirection += Vector3.forward; // 앞으로 이동
        if (Input.GetKey(KeyCode.S)) moveDirection += Vector3.back;    // 뒤로 이동
        if (Input.GetKey(KeyCode.A)) moveDirection += Vector3.left;    // 왼쪽 이동
        if (Input.GetKey(KeyCode.D)) moveDirection += Vector3.right;   // 오른쪽 이동

        // 입력 방향을 캐릭터의 로컬 방향으로 변환
        moveDirection = transform.TransformDirection(moveDirection);

        //회전 처리
        RotateWithMouse();

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
    }

    private void RotateWithMouse()  //마우스 회전에 따른 플레이어 시점 방향
    {
        // 마우스 이동 입력 감지
        float mouseX = Input.GetAxis("Mouse X");

        // 회전 각도 계산
        Vector3 rotation = new Vector3(0, mouseX, 0) * rotationSpeed * Time.deltaTime;

        // 현재 회전 상태에 회전 추가
        Quaternion deltaRotation = Quaternion.Euler(rotation);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.rotation * deltaRotation, rotationSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            SetAnimationState("animationState", (int)EPlayerState.JUMP);
            RIGIDBODY.velocity = new Vector3(RIGIDBODY.velocity.x, jumpPower, RIGIDBODY.velocity.z);
            isGrounded = false;
        }
    }
}
