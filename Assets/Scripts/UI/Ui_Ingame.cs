using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Ingame : BaseUi
{
    public Player player;
    public GameObject invenInterface;
    public GameObject optionInterface;
    public GameObject mapInterface;

    private void Start()
    {
        Shared.ui_Ingame = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            ToggleInterface(invenInterface);
        else if (Input.GetKeyDown(KeyCode.M))
            ToggleInterface(mapInterface);
        else if (Input.GetKeyDown(KeyCode.Escape))
            ToggleInterface(optionInterface);
    }
    private void ToggleInterface(GameObject uiElement)
    {
        if (isShow)
        {
            Hide(uiElement);
        }
        else
        {
            Show(uiElement);
        }
    }

    public void ShowInven() => ToggleInterface(invenInterface);
    public void ShowOption() => ToggleInterface(optionInterface);
    public void ShowMap() => ToggleInterface(mapInterface);
}
