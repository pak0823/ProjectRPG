//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerState_AttackState : IPlayerState
//{
//    public void Enter(Player player)
//    {
//        if (player.CanAttack())
//        {
//            player.SetAnimationState("animationState", (int)EPlayerState.ATTACK);
//            player.lastAttackTime = Time.time;
//        }
//    }

//    public void Update(Player player)
//    {
//        // 공격 상태에서의 로직 (예: 공격 애니메이션 재생)
//        //if (/* 공격이 끝났다면 */) // 조건을 적절히 설정
//        //{
//        //    player.ChangeState(new PlayerState_Idle());
//        //}
//    }

//    public void Exit(Player player) { }
//}
