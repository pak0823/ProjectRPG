using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    bool isAttack = false;
    Monster monster;
    AiMonster aiMonster;
    public Player player;

    public BoxCollider BOXCOLLIDER;
    public float attackDuration = 0.3f; // 공격 지속 시간

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        BOXCOLLIDER = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            isAttack = true;
        }
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
                if (player.isParrying)  //패링에 성공했을 시 몬스터에게 똑같은 대미지를 넘겨줌
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
