using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Monster monster;
    AiMonster aiMonster;
    Player player;

    //[SerializeField] private DynamicTextData critData;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public void OnTriggerEnter(Collider _collider)
    {
        if (_collider.gameObject.CompareTag("Enemy"))
        {
            aiMonster = _collider.gameObject.GetComponent<AiMonster>();
            
            if (aiMonster != null && player != null)
            {
                if (player.IsAttacking && aiMonster.CanHit())
                {
                    player.IsAttacking = false;
                    aiMonster.TakeDamage(player.GetGiveDamage);
                    DamageTextDisplay(aiMonster);
                    return;
                }
                else if(player.IsSkill)
                {
                    //여러번 대미지 주는 것을 허용하기에 IsSkill = false을 여기서 처리하지 않음
                    aiMonster.TakeDamage(player.GetGiveDamage);
                    DamageTextDisplay(aiMonster);
                }
            }
            else
            {
                if (aiMonster == null)
                    Debug.Log("aiMonster is null!");
                if (player == null)
                    Debug.Log("player is null!");
            }
        }
    }

    //private void OnTriggerExit(Collider _collider)
    //{
    //    if (_collider.gameObject.CompareTag("Enemy"))
    //    {
    //        //hasAttacked = false; // 충돌이 끝나면 공격 상태 리셋
    //    }
    //}

    //대미지 표시 메서드
    private void DamageTextDisplay(AiMonster _aimonster)
    {
        DynamicTextData data = _aimonster.transform.GetComponent<AiMonster>().textData;

        Vector3 destination = _aimonster.transform.position;

        destination.x += (Random.value - 0.5f);
        destination.y += (Random.value + 0.7f);
        destination.z += (Random.value - 0.5f);

        DynamicTextManager.CreateText(destination, player.GetGiveDamage.ToString(), data);

        //DynamicTextManager.CreateText(destination, "CRIT!", critData); 크리티컬 처리
    }
}
