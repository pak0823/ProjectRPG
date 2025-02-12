using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Ingame : MonoBehaviour
{
    public Player player;
    public Slider hpSlider; // 연결할 Hp 슬라이더
    public Slider staminaSlider;    //연결할 stamina 슬라이더

    public float maxHP = 0f; // 최대 Hp
    private float currentHp = 0f;    //현재 Hp
    public float maxStamina = 0f;   //최대 Stamina
    public float currentStamina = 0f; // 현재 Stamina

    public Slider cooldownSlider; // 연결할 슬라이더
    public float cooldownTime = 5f; // 스킬 쿨타임
    private bool isCooldown = false;



    private void Start()
    {
        Shared.Ui_Ingame = this;

        maxHP = player.maxHealth;
        maxStamina = player.maxStamina;

        currentHp = maxHP; // 현재 HP를 최대 HP로 초기화
        currentStamina = maxStamina;
        UpdateHPBar();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            InvenBtn();
        else if (Input.GetKeyDown(KeyCode.M))
            MapBtn();
        else if (Input.GetKeyDown(KeyCode.Escape))
            OptionBtn();
    }

    // HP를 감소시키는 메서드
    public void HealthBar(float _health)
    {
        currentHp = _health;
        currentHp = Mathf.Clamp(currentHp, 0, maxHP); // HP를 0과 maxHP 사이로 제한
        UpdateHPBar();
    }

    //public void IncreaseHealth(float _currentHp)
    //{
    //    currentHp = _currentHp;
    //}

    public void StaminaBar()
    {
        currentStamina = player.currentStamina;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina); // HP를 0과 maxHP 사이로 제한
        UpdateStaminaBar();
    }

    // HP 바 업데이트
    private void UpdateHPBar()
    {
        hpSlider.value = currentHp; // 슬라이더의 값을 현재 HP로 설정
    }
    private void UpdateStaminaBar()
    {
        staminaSlider.value = currentStamina;   // 슬라이더의 값을 현재 staimna로 설정
    }

    public void OptionBtn()
    {
        Debug.Log("Open Option");
    }

    public void InvenBtn()
    {
        Debug.Log("Open Inven");
    }

    public void MapBtn()
    {
        Debug.Log("Open Map");
    }

    public IEnumerator Cooldown()
    {
        isCooldown = true;
        cooldownSlider.maxValue = cooldownTime;
        cooldownSlider.value = cooldownTime; // 슬라이더를 쿨타임으로 설정

        while (cooldownSlider.value > 0)
        {
            cooldownSlider.value -= Time.deltaTime; // 슬라이더 값을 매 프레임 감소
            yield return null; // 다음 프레임까지 대기
        }

        cooldownSlider.value = 0; // 쿨타임이 끝나면 슬라이더를 0으로 설정
        isCooldown = false; // 쿨타임 상태 해제
    }
}
