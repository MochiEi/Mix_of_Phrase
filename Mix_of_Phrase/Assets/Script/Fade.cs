using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [SerializeField] private float FadeTime;

    public bool fade = false;
    public bool FadeIn = false;
    public bool FadeOut = false;

    public bool startFade = true;
    private Image fadeImage;
    // Start is called before the first frame update
    void Start()
    {
        fadeImage = GetComponent<Image>();
        fadeImage.raycastTarget = false;

        if (startFade)
        {
            fadeImage.fillAmount = 1;
            fade = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FadeIn && fade)
        {
            fadeImage.fillAmount -= Time.deltaTime * FadeTime;
            fadeImage.fillAmount = Math.Max(0, fadeImage.fillAmount);

            if (fadeImage.fillAmount <= 0)
            {
                FadeIn = false;
                fade = false;
            }
        }
        if (FadeOut && !fade)
        {
            fadeImage.fillAmount += Time.deltaTime * FadeTime;
            fadeImage.fillAmount = Math.Min(1, fadeImage.fillAmount);

            if(fadeImage.fillAmount >= 1)
            {
                FadeOut = false;
                fade = true;
            }
        }
    }
}
