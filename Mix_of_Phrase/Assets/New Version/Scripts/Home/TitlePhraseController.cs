using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePhraseController : MonoBehaviour
{
    [SerializeField] Text phraseBoardText;
    [SerializeField] Text text01;
    [SerializeField] Text text02;
    [SerializeField] Text text03;
    [SerializeField] Text text04;

    private int textCount;
    private string firstText, secondText;
    private string outputText;
        
    void Start()
    {
        firstText = "click";
        secondText = "phrase";
    }

    int a = 0;

    void Update()
    {
        outputText = firstText + " " + secondText;
        phraseBoardText.text = outputText;

        operationTable();
    }

    private void operationTable()
    {
        if(outputText == "game start")
        {

        }

        if(outputText == "exit game")
        {
            ExitGame();
        }
    }

    public void buttom01_Down()
    {
        if(textCount == 0)
        {
            firstText = text01.text;
            textCount++;
            return;
        }
        if(textCount == 1)
        {
            secondText = text01.text;
            textCount++;
            return;
        }
    }

    public void buttom02_Down()
    {
        if (textCount == 0)
        {
            firstText = text02.text;
            textCount++;
            return;
        }
        if (textCount == 1)
        {
            secondText = text02.text;
            textCount++;
            return;
        }
    }

    public void buttom03_Down()
    {
        if (textCount == 0)
        {
            firstText = text03.text;
            textCount++;
            return;
        }
        if (textCount == 1)
        {
            secondText = text03.text;
            textCount++;
            return;
        }
    }

    public void buttom04_Down()
    {
        if (textCount == 0)
        {
            firstText = text04.text;
            textCount++;
            return;
        }
        if (textCount == 1)
        {
            secondText = text04.text;
            textCount++;
            return;
        }
    }

    /// フレーズのリセット
    public void eraseButtom()
    {
        firstText = "click";
        secondText = "phrase";
        textCount = 0;
    }

    /// ゲーム終了
    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
