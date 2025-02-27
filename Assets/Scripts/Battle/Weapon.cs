using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Monster monster;
    AiMonster aiMonster;
    Player player;

    private bool isAttack = false;
    private bool isParrying = false;
    
    public float attackDuration = 0.3f; // 공격 지속 시간
    private float monsterAttackTime; //몬스터의 마지막 공격 시간
    private float parryingTrueTime = 0.5f; //패링 가능한 시간

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        if(player.currentState == EPlayerState.ATTACK || player.currentState == EPlayerState.SKILL)
        {
            isAttack = true;
        }
        else
            isAttack = false;
    }

    public void OnTriggerEnter(Collider _collider)
    {
        if (_collider.gameObject.CompareTag("Enemy"))
        {
            aiMonster = _collider.gameObject.GetComponent<AiMonster>();
            

            if (aiMonster != null && player != null)
            {
                if(isAttack)
                {
                    aiMonster.TakeDamage(player.giveDamage);
                    isAttack = false;
                }
                if(player.currentState == EPlayerState.DEFEND)
                {
                    Debug.Log(Time.time);
                    Debug.Log(OnAttackDetected + parryingTrueTime);
                    if (Time.time <= OnAttackDetected + parryingTrueTime)
                    {
                        Debug.Log("패링 반격공격 실행");
                        isParrying = true;
                        aiMonster.TakeDamage(monster.giveDamage);
                    }
                }
            }
            else
            {
                if (aiMonster == null)
                    Debug.Log("aiMonster is null!");
                if (player == null)
                    Debug.Log("player is null!");

                return;
            }
        }
    }

    // 적의 공격 감지 시 호출
    public float OnAttackDetected{ set { monsterAttackTime = value; } get { return monsterAttackTime; } }
    public bool IsParrying { set { isParrying = value; } get { return isParrying; } }


    protected void OnTriggerExit(Collider _collider)
    {
        //if (_collider.gameObject.CompareTag("Enemy"))
        //{
        //    isAttack = false;
        //}
    }
}
