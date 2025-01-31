using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitlePhraseActionContllore : MonoBehaviour
{
    [SerializeField] Text selectPhrase;
    private string phrase;

    void Update()
    {
        phrase = selectPhrase.text;

        if (phrase == "game start")
            GameStart();

        if (phrase == "exit game")
            ExitGame();
    }

    private void GameStart()
    {
        IsSelect.isActive = true;
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}