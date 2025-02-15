using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isAttack = false;
    Monster monster;
    AiMonster aiMonster;
    Player player;

    public float attackDuration = 0.3f; // 공격 지속 시간

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        if(player.currentState == EPlayerState.ATTACK)
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
                if (player.IsParrying)  //패링에 성공했을 시 몬스터에게 똑같은 대미지를 넘겨줌
                {
                    monster = _collider.gameObject.GetComponent<Monster>();
                    aiMonster.TakeDamage(monster.giveDamage);
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
    protected void OnTriggerExit(Collider _collider)
    {
        //if (_collider.gameObject.CompareTag("Enemy"))
        //{
        //    isAttack = false;
        //}
    }
}
