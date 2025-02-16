using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Ingame : MonoBehaviour
{
    public Player player;
    public bool isShow = false;

    public GameObject invenInterface;

    private void Start()
    {
        Shared.ui_Ingame = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            ShowInven();
        //else if (Input.GetKeyDown(KeyCode.M))
        //    MapBtn();
        //else if (Input.GetKeyDown(KeyCode.Escape))
        //    OptionBtn();
    }

    public void ShowInven()
    {
        if(isShow)
        {
            invenInterface.gameObject.SetActive(false);
            isShow = false;
        }
        else
        {
            invenInterface.gameObject.SetActive(true);
            isShow =true;
        }
    }
}
