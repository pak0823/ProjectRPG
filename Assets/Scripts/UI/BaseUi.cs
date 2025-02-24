using Unity.VisualScripting;
using UnityEngine;

public class BaseUi : MonoBehaviour
{
    public bool isShow = false;

    public virtual void Initialize() { }

    public virtual void UpdateUI() { }

    protected virtual void Start()
    {
        Debug.Log("Base Start");
        Shared.baseUi = this;
    }

    public virtual void Show()
    {
        isShow = true;
        Shared.cursorController.ToggleCursorOn();
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        isShow = false;
        Shared.cursorController.ToggleCursorOff();
        gameObject.SetActive(false);
    }

    
}
