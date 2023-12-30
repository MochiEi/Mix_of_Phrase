using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    Color Color = new Color(0f, 1f, 0f, 1.0f);
    //色を変えるボタン
    public Image button;

    [SerializeField, Header("ボタンの識別番号")]
    private bool buttonNo=false;

    [SerializeField, Header("表示するメニュー")]
    private GameObject MenuViews;

    private UIScript ui;

    // Start is called before the first frame update
    void Start()
    {
        button= button.GetComponent<Image>();
        ui= MenuViews.GetComponent<UIScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(ui.MenuViews== buttonNo) button.GetComponent<Image>().color = Color;
        else button.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }

}
