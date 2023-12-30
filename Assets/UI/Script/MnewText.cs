using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MnewText : MonoBehaviour
{
    [SerializeField, Header("キャンバス")]
    private GameObject Canvas;
    [SerializeField, Header("メニューの動きを司るもの")]
    private GameObject MnewMove;

    [SerializeField, Header("表示メニュー")]
    private Text MenuViews;

    UIScript uiScript;
    OutUI outline;

    //文字
    private string[] Name ={
    "a",
    "a"

    };
    bool[] IsName=new bool[2];
    //選んでいる順番
    int NameNo = 0;



    //文字
    private string[] Move ={
    "b",
    "b",
    "b"
    };
    bool[] IsMove = new bool[2];
    //選んでいる順番
    int MoveNo = 0;


    // Start is called before the first frame update
    void Start()
    {
        uiScript= Canvas.GetComponent<UIScript>();
        outline = MnewMove.GetComponent<OutUI>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (outline.MenuWhereabouts)
            {
                if (uiScript.MenuViews && MoveNo > 0) MoveNo--;
                else if (NameNo > 0) NameNo--;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (outline.MenuWhereabouts)
            {
                if (uiScript.MenuViews && MoveNo < Move.Length - 1) MoveNo++;
                else if (NameNo < Name.Length - 1) NameNo++;
            }
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MenuViews.text = "";
        switch (uiScript.MenuViews)
        {
            case true:
                for (int i = 0; i<Move.Length;i++)
                {
                    MenuViews.text += Move[i];

                    if (i == MoveNo) MenuViews.text += "　⇦";
                    if (i != Move.Length - 1) MenuViews.text += "\n";
                }
                break;
            case false:
                for (int i = 0; i < Name.Length; i++)
                {
                    MenuViews.text += Name[i];

                    if (i == NameNo) MenuViews.text += "　⇦";
                    if (i != Name.Length - 1) MenuViews.text += "\n";
                }
                break;
        }
    }
}
