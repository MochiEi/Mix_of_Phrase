using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startFade : MonoBehaviour
{
    ///// 動作するオブジェクト群 /////
    public GameObject[] inactive;
    public GameObject[] FadeOutText;
    public GameObject[] FadeOut;

    public float fadeSpeed = 0.5f;
    
    ///// 動作用リスト /////
    private List<Text> OutTexts;
    private List<SpriteRenderer> OutSpriteRenderer;

    ///// シーン切り替え用変数 /////
    public bool next = false;

    void Start()
    {
        FadeoutInit();
    }

    void Update()
    {
        startSystem startSystem = this.GetComponent<startSystem>();

        if (startSystem.start)
        {
            Fadeout();
        }
    }  

    void FadeoutInit()
    {
        OutTexts = new List<Text>();
        OutSpriteRenderer = new List<SpriteRenderer>();

        foreach (GameObject fade in FadeOutText)
        {
            OutTexts.AddRange(fade.GetComponentsInChildren<Text>());
        }
        foreach (GameObject fade in FadeOut)
        {
            OutSpriteRenderer.AddRange(fade.GetComponentsInChildren<SpriteRenderer>());
        }
    }

    void Fadeout()
    {
        foreach (Text text in OutTexts)
        {
            Color color = text.color;

            color.a -= fadeSpeed * Time.deltaTime;
            color.a = Mathf.Max(0, color.a);

            text.color = color;

            ///// 透明度0で非アクティブ /////
            if (color.a == 0)
            {
                text.transform.parent.gameObject.SetActive(false);
            }
        }
        foreach (SpriteRenderer alpha in OutSpriteRenderer)
        {
            Color color = alpha.color;

            color.a -= fadeSpeed * Time.deltaTime;
            color.a = Mathf.Max(0, color.a);

            alpha.color = color;

            ///// 透明度0で非アクティブ /////
            if (color.a == 0)
            {
                alpha.transform.parent.gameObject.SetActive(false);

                ///// 便乗して操作用空オブジェクトを非アクティブ /////
                Inactive();
            }
        }
    }

    void Inactive()
    {
        foreach (GameObject obj in inactive)
        {
            obj.SetActive(false);
        }

        next = true;
    }
}