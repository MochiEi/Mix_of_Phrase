using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField, Header("メニューの存在状態")]
    private GameObject MenuWhereabouts;

    [HideInInspector]//表示するメニュー
    public bool MenuViews = false;

    private OutUI ui;
    // Start is called before the first frame update
    void Start()
    {
        ui = MenuWhereabouts.GetComponent<OutUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            if (ui.MenuWhereabouts)
            {
                MenuViews = !MenuViews;
            }
        }

    }
}
