using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwitchScreenContoroller : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] PhraseBoardController controller;
    //false = タイトル // true = セレクト
    public bool isSelect;
    public bool isMove;
    public bool start;

    void Start()
    {

    }

    void Update()
    {
        if (!isSelect && start)
        {
            start = false;
            isMove = true;
            var sequence = DOTween.Sequence();
            sequence
                .Append(cam.transform.DOMoveY(-10.0f, 3.0f)).SetEase(Ease.OutSine)
                .OnComplete(() =>
                {
                    controller.ErasePhrase();
                    isSelect = true;
                    isMove = false;
                });
        }

        if (isSelect && start)
        {
            start = false;
            isMove = false;
            var sequence = DOTween.Sequence();
            sequence
                .Append(cam.transform.DOMoveY(0.0f, 3.0f)).SetEase(Ease.OutSine)
                .OnComplete(() =>
                {
                    isSelect = false;
                });
        }
    }

    public void ToTitle()
    {
        if (isSelect) start = true;
    }
}