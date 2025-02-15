using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Player player;
    public Slider hpSlider; // 연결할 Hp 슬라이더

    private float maxHP = 0f; // 최대 Hp
    private float currentHp = 0f;    //현재 Hp

    private void Start()
    {
        Shared.hpBar = this;

        maxHP = player.GetMaxHp;
        currentHp = player.GetCurrentHp;
    }

    public void HealthBar(float _health)
    {
        currentHp = _health;
        currentHp = Mathf.Clamp(currentHp, 0, maxHP); // HP를 0과 maxHP 사이로 제한
        UpdateHPBar();
    }
    private void UpdateHPBar()
    {
        hpSlider.value = currentHp; // 슬라이더의 값을 현재 HP로 설정
    }
}
