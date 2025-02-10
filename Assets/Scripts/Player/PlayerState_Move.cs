//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerState_Move : IPlayerState
//{
//    public void Enter(Player player)
//    {
//        // 이동 상태 초기화 로직
//        player.SetAnimationState("animationState", 0);
//    }

//    public void Update(Player player)
//    {
//        Vector3 moveDirection = Vector3.zero;

//        // W, A, S, D 키 입력 처리
//        if (Input.GetKey(KeyCode.W)) moveDirection += Vector3.forward;
//        if (Input.GetKey(KeyCode.S)) moveDirection += Vector3.back;
//        if (Input.GetKey(KeyCode.A)) moveDirection += Vector3.left;
//        if (Input.GetKey(KeyCode.D)) moveDirection += Vector3.right;

//        // 카메라 방향에 맞춰 이동 처리
//        Vector3 cameraForward = Camera.main.transform.forward;
//        cameraForward.y = 0;
//        cameraForward.Normalize();

//        if (moveDirection != Vector3.zero)
//        {
//            Vector3 desiredDirection = cameraForward * moveDirection.z + Camera.main.transform.right * moveDirection.x;

//            // 캐릭터 회전
//            Quaternion toRotation = Quaternion.LookRotation(desiredDirection);
//            player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, toRotation, player.rotationSpeed * Time.deltaTime);

//            // Rigidbody.velocity를 사용하여 이동
//            float moveSpeed = Mathf.Lerp(player.walkSpeed, player.runSpeed, Input.GetAxis("Sprint"));
//            player.RIGIDBODY.velocity = desiredDirection * moveSpeed;
//        }

//        if (moveDirection == Vector3.zero)
//        {
//            player.ChangeState(new PlayerState_Idle());
//        }
//    }

//    public void Exit(Player player) { }
//}
