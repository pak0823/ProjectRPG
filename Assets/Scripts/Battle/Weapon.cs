using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    bool isAttack = false;
    Monster monster;
    public Player player;

    private void Awake()
    {
        //player = GetComponent<Player>();
        player = GetComponentInParent<Player>();
    }

    public void OnTriggerEnter(Collider _collider)
    {
        if (_collider.gameObject.CompareTag("Enemy") && !isAttack)
        {
            monster = _collider.gameObject.GetComponent<Monster>();

            isAttack = true;

            if (monster != null && player != null)
            {
                monster.TakeDamage(player.giveDamage);
            }
            else
            {
                Debug.Log($"monster:{monster}, player:{player}");
            }

            Debug.Log("Enemy is Damaged!");
        }
    }
    protected void OnTriggerExit(Collider _collider)
    {
        if (_collider.gameObject.CompareTag("Enemy"))
        {
            isAttack = false;
            Debug.Log("Enemy Damaged after!");
        }
    }
}
