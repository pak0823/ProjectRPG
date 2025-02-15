using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Ingame : MonoBehaviour
{
    public Player player;

    private void Start()
    {
        Shared.ui_Ingame = this;
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.I))
        //    InvenBtn();
        //else if (Input.GetKeyDown(KeyCode.M))
        //    MapBtn();
        //else if (Input.GetKeyDown(KeyCode.Escape))
        //    OptionBtn();
    }

    

    //public void OptionBtn()
    //{
    //    Debug.Log("Open Option");
    //}

    //public void InvenBtn()
    //{
    //    Debug.Log("Open Inven");
    //}

    //public void MapBtn()
    //{
    //    Debug.Log("Open Map");
    //}
}
