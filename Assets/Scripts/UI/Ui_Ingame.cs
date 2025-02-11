using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Ingame : MonoBehaviour
{
    public Slider hpSlider; // 연결할 슬라이더
    public float maxHP = 100f; // 최대 HP
    private float currentHP;

    private void Start()
    {
        currentHP = maxHP; // 현재 HP를 최대 HP로 초기화
        UpdateHPBar();
    }

    // HP를 감소시키는 메서드
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP); // HP를 0과 maxHP 사이로 제한
        UpdateHPBar();
    }

    // HP 바 업데이트
    private void UpdateHPBar()
    {
        hpSlider.value = currentHP; // 슬라이더의 값을 현재 HP로 설정
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10f);
            Debug.Log("space");
        }
    }
}
