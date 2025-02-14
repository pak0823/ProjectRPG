using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillCooldownUi : BaseUi
{
    public Slider[] skillCooldownSliders; // 스킬 쿨타임 슬라이더 배열
    public float[] cooldownTimes; // 각 스킬의 쿨타임
    private bool[] isCooldown; // 각 스킬의 쿨타임 상태

    private void Start()
    {
        isCooldown = new bool[cooldownTimes.Length];
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        for (int i = 0; i < skillCooldownSliders.Length; i++)
        {
            skillCooldownSliders[i].maxValue = cooldownTimes[i]; // 최대값 설정
            skillCooldownSliders[i].value = 0; // 초기값 설정
            isCooldown[i] = false; // 쿨타임 상태 초기화
        }
    }

    public void StartCooldown(int skillIndex)
    {
        if (!isCooldown[skillIndex])
        {
            StartCoroutine(Cooldown(skillIndex));
        }
    }

    private IEnumerator Cooldown(int _skillIndex)
    {
        isCooldown[_skillIndex] = true;
        skillCooldownSliders[_skillIndex].value = cooldownTimes[_skillIndex]; // 슬라이더를 쿨타임으로 설정

        while (skillCooldownSliders[_skillIndex].value > 0)
        {
            skillCooldownSliders[_skillIndex].value -= Time.deltaTime; // 슬라이더 값을 매 프레임 감소
            yield return null; // 다음 프레임까지 대기
        }

        skillCooldownSliders[_skillIndex].value = 0; // 쿨타임이 끝나면 슬라이더를 0으로 설정
        isCooldown[_skillIndex] = false; // 쿨타임 상태 해제
    }
}