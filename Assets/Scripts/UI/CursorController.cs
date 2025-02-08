using UnityEngine;

public class CursorController : MonoBehaviour
{
    private void Start()
    {
        // 게임 시작 시 마우스 포인터 숨기기
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // 마우스 포인터를 중앙에 고정
    }

    private void Update()
    {
        // ESC 키 입력 시 마우스 포인터 보이기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursorVisibility();
        }
    }

    private void ToggleCursorVisibility()
    {
        // 현재 가시성을 반전
        if (Cursor.visible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; // 다시 중앙에 고정
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // 고정 해제
        }
    }
}
