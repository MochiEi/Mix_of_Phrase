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

    /// �t�F�[�h�������Ă���
    [HideInInspector]
    public bool fadeIn;
    /// �t�F�[�h�����Ă���
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

        /// �t�F�[�h�C���E�A�E�g���X�L�b�v
        if (testMode)
        {
            amount = 0;
            isActive = false;
        }
    }

    void Update()
    {
        /// �J�N�t���h�~�̂���5�t���[���X�L�b�v
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

        /// �t�F�[�h���̓N���b�N���삵�Ă��C���[�W���ז����Ė���
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