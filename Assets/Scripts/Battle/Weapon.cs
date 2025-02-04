using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    bool isAttack = false;

    public void OnTriggerEnter(Collider _collider)
    {
        if (_collider.gameObject.CompareTag("Enemy") && !isAttack)
        {
            isAttack = true;
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
