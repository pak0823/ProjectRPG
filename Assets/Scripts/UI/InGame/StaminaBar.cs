using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField]private Player player;   //플레이어
    [SerializeField]private Slider staminaSlider;    //연결할 stamina 슬라이더

    private float maxStamina = 0f;   //최대 Stamina
    private float currentStamina = 0f; // 현재 Stamina
    void Start()
    {
        Shared.staminaBar = this;
        maxStamina = player.GetMaxStamina;
        currentStamina = player.GetCurrentStamina;
    }

    public void Stamina(float _stamina)
    {
        currentStamina = _stamina;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina); // HP를 0과 maxHP 사이로 제한
        UpdateStaminaBar();
    }

    private void UpdateStaminaBar()
    {
        staminaSlider.value = currentStamina;   // 슬라이더의 값을 현재 staimna로 설정
    }
}
