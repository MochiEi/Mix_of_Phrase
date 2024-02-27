using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhraseWindow : MonoBehaviour
{
    [SerializeField] GameObject TextFrame;
    [SerializeField] GameObject cam;

    public Text text;
    private string text1;
    private string text2;

    private Vector3 pos;
    private bool isWindow;
   
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        isWindow = true;

        text = TextFrame.GetComponent<Text>();
        text1 = "";
        text2 = "box";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isWindow = !isWindow;
        }
        if (isWindow)
        {
            pos.x -= Time.deltaTime * 50;
            pos.x = Math.Max(-13f + cam.transform.position.x, pos.x);
        }
        if (!isWindow)
        {
            pos.x += Time.deltaTime * 50;
            pos.x = Math.Min(-4.23f + cam.transform.position.x, pos.x);
        }

        transform.position = pos;

        text.text = text1 + "  " + text2;
    }

    public void button1()
    {
        text1 = "create";
    }
    public void button2()
    {
        text1 = "break";
    }
}
