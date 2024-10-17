using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleFadeController : MonoBehaviour
{
    private int skipFrame;

    [SerializeField] SharedVariableController shared;

    [SerializeField] Image bluckFade;
    private FadeController fadeController;

    [SerializeField] GameObject whiteFade;
    private SpriteRenderer spriteRenderer;
    public bool isActive;

    /// フェードが引いてる状態
    private bool fadeIn;
    /// フェードが閉じてる状態
    private bool fadeOut;

    private float alpha;
    [SerializeField] float fadeSpeed;

    private bool initGame;

    void Start()
    {
        fadeController = bluckFade.GetComponent<FadeController>();

        whiteFade.SetActive(true); 
        spriteRenderer = whiteFade.GetComponent<SpriteRenderer>();

        alpha = 1.0f;
        fadeSpeed = -Mathf.Abs(fadeSpeed);
        isActive = true;
    }

    void Update()
    {
        /// カク付き防止のため5フレームスキップ
        if (skipFrame < 5)
        {
            skipFrame++;
            return;
        }

        initGame = shared.initGame;
        /// 初期起動なら黒フェード無効
        if (initGame)
        {
            bluckFade.fillAmount = 0;
        }

        if (isActive && bluckFade.fillAmount == 0)
        {
            alpha += fadeSpeed * Time.deltaTime;

            alpha = Mathf.Clamp(alpha, 0, 1);

            if (alpha == 1 || alpha == 0)
                isActive = false;
        }

        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, alpha);

        whiteFade.SetActive(alpha != 0);
        fadeChake();
        fadeDirection();
    }

    private void fadeChake()
    {
        fadeIn = (alpha == 0);
        fadeOut = (alpha == 1);
    }

    private void fadeDirection()
    {
        if (fadeIn)
            fadeSpeed = Mathf.Abs(fadeSpeed);
        else if (fadeOut)
            fadeSpeed = -Mathf.Abs(fadeSpeed);
    }
}