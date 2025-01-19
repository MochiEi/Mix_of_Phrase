using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitlePhraseActionContllore : MonoBehaviour
{
    [SerializeField] Transform cam;
    private TitleCameraController camController;
    private TitlePhraseActionContllore contllore;

    [SerializeField] Text selectPhrase;
    private string phrase;

    void Start()
    {
        camController = cam.GetComponent<TitleCameraController>();
        contllore = GetComponent<TitlePhraseActionContllore>();
    }

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
        camController.enabled = true;
        contllore.enabled = false;
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
