using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectFade : MonoBehaviour
{
    ///// 動作するオブジェクト群 /////
    public GameObject[] inactive;
    public GameObject[] FadeInText;
    public GameObject[] FadeIn;

    public float fadeSpeed = 0.5f;
    
    ///// 動作用リスト /////
    private List<Text> OutTexts;
    private List<SpriteRenderer> OutSpriteRenderer;

    ///// シーン切り替え用変数 /////
    public bool next = false;
    private bool select;

    void Start()
    {
        select = true;
        FadeoutInit();
    }

    void Update()
    {
        if (select)
        {
            Fadein();
        }
    }  

    void FadeoutInit()
    {
        OutTexts = new List<Text>();
        OutSpriteRenderer = new List<SpriteRenderer>();

        foreach (GameObject fade in FadeInText)
        {
            OutTexts.AddRange(fade.GetComponentsInChildren<Text>());
        }
        foreach (GameObject fade in FadeIn)
        {
            OutSpriteRenderer.AddRange(fade.GetComponentsInChildren<SpriteRenderer>());
        }

        foreach (Text text in OutTexts)
        {
            Color color = text.color;

            color.a = 0;

            text.color = color;
        }
        foreach (SpriteRenderer alpha in OutSpriteRenderer)
        {
            Color color = alpha.color;

            color.a = 0;

            alpha.color = color;
        }
    }

    void Fadein()
    {
        foreach (Text text in OutTexts)
        {
            Color color = text.color;

            color.a += fadeSpeed * Time.deltaTime;
            color.a = Mathf.Min(1, color.a);

            text.color = color;
        }
        foreach (SpriteRenderer alpha in OutSpriteRenderer)
        {
            Color color = alpha.color;

            color.a += fadeSpeed * Time.deltaTime;
            color.a = Mathf.Min(1, color.a);

            alpha.color = color;

            if (color.a == 1)
            {
                Inactive();
            }
        }
    }

    void Inactive()
    {
        select = false;
    }
}