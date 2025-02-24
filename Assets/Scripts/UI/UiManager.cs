using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private Stack<BaseUi> uiStack = new Stack<BaseUi>();

    private void Start()
    {
        Shared.uiManager = this;
    }
    public void ShowInterface(BaseUi uiElement)
    {
        // 현재 열려 있는 UI가 있다면
        if (uiStack.Count > 0)
        {
            var currentUi = uiStack.Peek(); // 스택의 가장 위에 있는 UI
            if (currentUi == uiElement)
            {
                // 이미 열려 있는 UI를 닫기
                HideInterface();
                return;
            }
            currentUi.Hide(); // 현재 UI 숨기기
        }

        // 새로운 UI를 활성화하고 스택에 추가합니다.
        uiElement.Show();
        uiStack.Push(uiElement);
    }

    public void HideInterface()
    {
        if (uiStack.Count > 0)
        {
            var topUi = uiStack.Pop(); // 스택의 맨 위에 있는 UI를 꺼냄
            topUi.Hide(); // UI 숨기기

            // 다음 UI를 다시 활성화합니다.
            if (uiStack.Count > 0)
            {
                var nextUi = uiStack.Peek();
                nextUi.Show();
            }
        }
    }

    // 현재 열려 있는 UI가 있는지 확인하는 메서드 추가
    public bool IsAnyInterfaceOpen()
    {
        return uiStack.Count > 0;
    }
}
