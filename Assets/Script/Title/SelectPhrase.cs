using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SelectPhrase : MonoBehaviour
{
    [SerializeField] private GameObject selectText; 
    [SerializeField] private CanvasGroup canvas;

    Text text;
    string text1;
    string text2;

    [SerializeField] private bool startFade;
    [SerializeField] private float FadeTime;
    private int FadeCount;
    private int textCount;

    private int coutframe;

    // Start is called before the first frame update
    void Start()
    {
        text = selectText.GetComponent<Text>();
        text1 = "click";
        text2 = "phrase";
        
        if(!startFade) canvas.alpha = 0;
        FadeCount = 0;
        textCount = 1;
    }

    // Update is called once per frame
    void Update()
    {   
        text.text = text1 + " " + text2;

        if (coutframe > 5)
        {
            switch (FadeCount)
            {
                case 0:
                    fadeIn();
                    break;
                case 1:
                    break;
                case 2:
                    fadeOut();
                    break;
            }
        }
        coutframe++;
    }
    public void decision()
    {

        if (text.text == "game start")
        {
            FadeCount++;
        }
        //else if (text.text == "game staff")
        //{
        //    print(text.text);
        //}
        else if (text.text == "exit game")
        {
            ExitGame();
        }
        else
        {
            text1 = "click";
            text2 = "phrase";
            textCount = 1;
        }
    }
    private void fadeIn()
    {
        canvas.alpha += Time.deltaTime * FadeTime;
        canvas.alpha = Math.Min(1, canvas.alpha);
        if(canvas.alpha >= 1) FadeCount++;
    }
    private void fadeOut()
    {
        canvas.alpha -= Time.deltaTime * FadeTime;
        canvas.alpha = Math.Max(0, canvas.alpha);
        if (canvas.alpha <= 0) SceneManager.LoadScene("Select");
    }
    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
    public void Push1()
    {
        if (canvas.alpha < 1) return;

        if (textCount == 2)
        {
            text2 = "game";
            textCount++;
        }
        if (textCount == 1)
        {
            text1 = "game";
            text2 = null;
            textCount++;
        }
    }
    public void Push2()
    {
        if (canvas.alpha < 1) return;

        if (textCount == 2)
        {
            text2 = "start";
            textCount++;
        }
        if (textCount == 1)
        {
            text1 = "start";
            text2 = null;
            textCount++;
        }
    }
    public void Push3()
    {
        if (canvas.alpha < 1) return;

        if (textCount == 2)
        {
            text2 = "staff";
            textCount++;
        }
        if (textCount == 1)
        {
            text1 = "staff";
            text2 = null;
            textCount++;
        }
    }
    public void Push4()
    {
        if (canvas.alpha < 1) return;

        if (textCount == 2)
        {
            text2 = "exit";
            textCount++;
        }
        if (textCount == 1)
        {
            text1 = "exit";
            text2 = null;
            textCount++;
        }
    }
}