using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Ingame : BaseUi
{
    public BaseUi invenInterface;
    public BaseUi optionInterface;
    public BaseUi mapInterface;

    protected override void Start()
    {
        Shared.ui_Ingame = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            Shared.uiManager.ShowInterface(invenInterface);
        else if (Input.GetKeyDown(KeyCode.M))
            Shared.uiManager.ShowInterface(mapInterface);
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 현재 열려 있는 UI가 있는지 확인
            if (Shared.uiManager.IsAnyInterfaceOpen())
            {
                Shared.uiManager.HideInterface(); // 열려 있는 UI 닫기
            }
            else
            {
                Shared.uiManager.ShowInterface(optionInterface); // optionInterface 열기
            }
        }
    }
}
