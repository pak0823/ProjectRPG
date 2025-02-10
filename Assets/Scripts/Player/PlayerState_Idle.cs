//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerState_Idle : IPlayerState
//{
//    public void Enter(Player player)
//    {
//        player.SetAnimationState("animationState", (int)EPlayerState.IDLE);
//        player.SetAnimationMove("moveSpeed", 0);
//    }

//    public void Update(Player player)
//    {
//        // Idle 상태에서의 로직
//        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
//        {
//            player.ChangeState(new PlayerState_Move());
//        }
//    }

//    public void Exit(Player player) { }
//}
