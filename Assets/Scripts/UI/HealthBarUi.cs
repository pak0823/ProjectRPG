using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarUi : BaseUi
{
    public Slider hpSlider;
    public Player player;

    public override void Initialize()
    {
        base.Initialize();
        UpdateUI();
    }

    public override void UpdateUI()
    {
        base.UpdateUI();
        //hpSlider.value = player.currentHealth; // 플레이어의 현재 HP를 슬라이더에 반영
    }
}
