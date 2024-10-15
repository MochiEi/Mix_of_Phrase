using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    private int skipFrame;

    private Image fadeImage;
    private float amount;

    [SerializeField] float fadeSpeed;
    public bool isActive;

    /// フェードが引いてる状態
    [HideInInspector]
    public bool fadeIn;
    /// フェードが閉じてる状態
    [HideInInspector]
    public bool fadeOut;

    [Header("")]
    [SerializeField] bool testMode;

    void Start()
    {
        fadeImage = GetComponent<Image>();
        amount = fadeImage.fillAmount;

        amount = 1;
        fadeSpeed = -Mathf.Abs(fadeSpeed);
        isActive = true;

        /// フェードイン・アウトをスキップ
        if (testMode)
        {
            amount = 0;
            isActive = false;
        }
    }

    void Update()
    {
        /// カク付き防止のため5フレームスキップ
        if(skipFrame < 5)
        {
            skipFrame++;
            return;
        }

        if (isActive)
            amount += fadeSpeed * Time.deltaTime;

        amount = Mathf.Clamp(amount, 0, 1);

        if (amount == 1 || amount == 0)
            isActive = false;

        /// フェード中はクリック操作してもイメージが邪魔して無効
        if (fadeIn)
            fadeImage.raycastTarget = false;
        else
            fadeImage.raycastTarget = true;

        fadeImage.fillAmount = amount;
        fadeChake();
        fadeDirection();
    }

    private void fadeChake()
    {
        fadeIn = (amount == 0);
        fadeOut = (amount == 1);
    }

    private void fadeDirection()
    {
        if (fadeIn)
            fadeSpeed = Mathf.Abs(fadeSpeed);
        else if (fadeOut)
            fadeSpeed = -Mathf.Abs(fadeSpeed);
    }
}