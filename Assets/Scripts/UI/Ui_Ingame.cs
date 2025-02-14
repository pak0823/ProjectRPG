using System.Collections;
using System.Collections.Generic;
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

    public Slider[] skillCooldownSliders; // 스킬 쿨타임 슬라이더 배열
    public float[] cooldownTimes; // 각 스킬의 쿨타임
    public bool[] isCooldown; // 각 스킬의 쿨타임 상태



    private void Start()
    {
        Shared.Ui_Ingame = this;

        //maxHP = player.maxHealth;
        maxStamina = player.maxStamina;

        currentHp = maxHP; // 현재 HP를 최대 HP로 초기화
        currentStamina = maxStamina;
        UpdateHPBar();

        // 쿨타임 배열 초기화
        isCooldown = new bool[cooldownTimes.Length];
        for (int i = 0; i < isCooldown.Length; i++)
        {
            isCooldown[i] = false;
            skillCooldownSliders[i].maxValue = cooldownTimes[i];
            skillCooldownSliders[i].value = 0; // 초기값 설정
        }
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.I))
        //    InvenBtn();
        //else if (Input.GetKeyDown(KeyCode.M))
        //    MapBtn();
        //else if (Input.GetKeyDown(KeyCode.Escape))
        //    OptionBtn();
    }

    // HP를 감소시키는 메서드
    public void HealthBar(float _health)
    {
        currentHp = _health;
        currentHp = Mathf.Clamp(currentHp, 0, maxHP); // HP를 0과 maxHP 사이로 제한
        UpdateHPBar();
    }

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

    //public void OptionBtn()
    //{
    //    Debug.Log("Open Option");
    //}

    //public void InvenBtn()
    //{
    //    Debug.Log("Open Inven");
    //}

    //public void MapBtn()
    //{
    //    Debug.Log("Open Map");
    //}

    // 스킬 쿨타임 시작
    public void StartCooldown(int skillIndex)
    {
        if (!isCooldown[skillIndex])
        {
            StartCoroutine(Cooldown(skillIndex));
        }
    }

    // 특정 스킬의 쿨타임 처리
    private IEnumerator Cooldown(int skillIndex)
    {
        isCooldown[skillIndex] = true;
        skillCooldownSliders[skillIndex].value = cooldownTimes[skillIndex]; // 슬라이더를 쿨타임으로 설정

        while (skillCooldownSliders[skillIndex].value > 0)
        {
            skillCooldownSliders[skillIndex].value -= Time.deltaTime; // 슬라이더 값을 매 프레임 감소
            yield return null; // 다음 프레임까지 대기
        }

        skillCooldownSliders[skillIndex].value = 0; // 쿨타임이 끝나면 슬라이더를 0으로 설정
        isCooldown[skillIndex] = false; // 쿨타임 상태 해제
    }
}
