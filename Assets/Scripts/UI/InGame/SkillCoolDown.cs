using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolDown : MonoBehaviour
{
    public Player player;

    public Slider[] skillCooldownSliders; // 스킬 쿨타임 슬라이더 배열
    public float[] cooldownTimes; // 각 스킬의 쿨타임
    public bool[] isCooldown; // 각 스킬의 쿨타임 상태

    private void Start()
    {
        Shared.skillCoolDown = this;

        InitSetting();
    }

    private void InitSetting()
    {
        // 쿨타임 배열 초기화
        isCooldown = new bool[cooldownTimes.Length];
        for (int i = 0; i < isCooldown.Length; i++)
        {
            isCooldown[i] = false;
            skillCooldownSliders[i].maxValue = cooldownTimes[i];
            skillCooldownSliders[i].value = 0; // 초기값 설정
        }
    }

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
