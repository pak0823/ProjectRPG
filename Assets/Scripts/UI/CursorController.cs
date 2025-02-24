using UnityEngine;

public class CursorController : MonoBehaviour
{
    private void Start()
    {
        Shared.cursorController = this;

        // 게임 시작 시 마우스 포인터 숨기기
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // 마우스 포인터를 중앙에 고정
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (Cursor.visible)
                ToggleCursorOff();
            else
                ToggleCursorOn();
        }
            
    }

    public void ToggleCursorOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // 고정 해제
    }
    public void ToggleCursorOff()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // 다시 중앙에 고정
    }
}
