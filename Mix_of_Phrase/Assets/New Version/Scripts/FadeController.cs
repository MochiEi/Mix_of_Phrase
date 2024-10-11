using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    private Image fadeImage;

    [SerializeField] float fadeTime;
    private float amount;

    public bool isFade;
    public bool fadeIn;
    public bool fadeOut;
    [SerializeField] bool testMode;

    void Start()
    {
        fadeImage = GetComponent<Image>();

        if (!testMode)
            fadeImage.fillAmount = 1;
    }

    void Update()
    {
        amount = fadeImage.fillAmount;
        fadeChake();

        fadeImage.raycastTarget = false;
    }

    private void fadeChake()
    {
        fadeIn = (fadeImage.fillAmount == 1);
        fadeOut = (fadeImage.fillAmount == 0);
    }
}
