using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StaminaBarUi : BaseUi
{
    public Slider staminaSlider; // 스태미너 슬라이더
    public Player player; // 플레이어 참조

    public override void Initialize()
    {
        base.Initialize();
        UpdateUI(); // 초기 UI 업데이트
    }

    public override void UpdateUI()
    {
        base.UpdateUI();
        staminaSlider.value = player.currentStamina; // 플레이어의 현재 스태미너를 슬라이더에 반영
    }
}