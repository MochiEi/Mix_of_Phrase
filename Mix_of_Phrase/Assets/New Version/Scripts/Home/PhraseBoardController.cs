using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhraseBoardController : MonoBehaviour
{
    [SerializeField] SwitchScreenContoroller sceneContoroller;
    private Text outputText;

    private int textCount;
    private string firstText, secondText, text;

    void Start()
    {
        foreach (Transform child in this.transform)
        {
            if (child.gameObject.name == "Text")
                outputText = child.GetComponent<Text>();
        }

        textCount = 0;
        firstText = "click";
        secondText = "phrase";
    }

    void Update()
    {
        text = firstText + " " + secondText;
        outputText.text = text;

        OperationTable();
    }

    private void OperationTable()
    {
        if (text == "game start")
        {
            if (!sceneContoroller.isMove)
                sceneContoroller.start = true;
        }

        if (text == "exit game")
        {
            ExitGame();
        }
    }

    public void Buttom_Game()
    {
        if (textCount == 0)
        {
            firstText = "game";
            secondText = "";
            textCount++;
            return;
        }
        if (textCount == 1)
        {
            secondText = "game";
            textCount++;
            return;
        }
    }

    public void Buttom_Start()
    {
        if (textCount == 0)
        {
            firstText = "start";
            secondText = "";
            textCount++;
            return;
        }
        if (textCount == 1)
        {
            secondText = "start";
            textCount++;
            return;
        }
    }

    public void Buttom_Exit()
    {
        if (textCount == 0)
        {
            firstText = "exit";
            secondText = "";
            textCount++;
            return;
        }
        if (textCount == 1)
        {
            secondText = "exit";
            textCount++;
            return;
        }
    }

    public void ErasePhrase()
    {
        firstText = "click";
        secondText = "phrase";
        textCount = 0;
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}