using Unity.VisualScripting;
using UnityEngine;

public class BaseUi : MonoBehaviour
{
    public bool isShow = false;

    public virtual void Initialize()
    {
        
    }

    public virtual void UpdateUI() 
    { 
    
    }

    protected void Start()
    {
        Debug.Log("base Start");
        Shared.baseUi = this;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // 마우스 포인터를 중앙에 고정
    }

    public virtual void Show(GameObject _showObject)
    {
        isShow = true;
        ToggleCursorOn();
        _showObject.gameObject.SetActive(true);
    }
    public virtual void Hide(GameObject _showObject) 
    {
        isShow = false;
        ToggleCursorOff();
        _showObject.gameObject.SetActive(false);
    }
    private void ToggleCursorOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // 고정 해제
    }
    private void ToggleCursorOff()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // 다시 중앙에 고정
    }


}
