using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PhraseController : MonoBehaviour
{
    [SerializeField] selectChangePhrase changePhrase;
    private enum selectChangePhrase
    {
        First, Second
    };

    //------------------- First --------------------//
    private Transform firstPhrase;
    private Text[] firstPhrases;
    private int firstPhraseCount;

    //------------------- Second -------------------//
    private Transform secondPhrase;
    private Text[] secondPhrases;
    private int secondPhraseCount;

    //------------------- Output -------------------//
    private string executionWord;

    private RectTransform frame;

    private float alpha;
    private Color displayColor;
    private Color hiddenColor;

    private Tween frameScale;
    private float interval;

    private float width;
    private float height;

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "FirstPhrase")
                firstPhrase = child;
            if (child.gameObject.name == "SecondPhrase")
                secondPhrase = child;
            if (child.gameObject.name == "Frame")
                frame = child.GetComponent<RectTransform>();
        }

        firstPhrases = firstPhrase.Cast<Transform>().Select(phrase => phrase.GetComponent<Text>()).ToArray();
        secondPhrases = secondPhrase.Cast<Transform>().Select(phrase => phrase.GetComponent<Text>()).ToArray();

        firstPhraseCount = 0;
        secondPhraseCount = 0;

        alpha = 0;
        displayColor = new Color(0, 0, 0, 1.0f);
        hiddenColor = new Color(0, 0, 0, 0);

        width = frame.rect.width + 10;

        DoTween();
    }

    void Update()
    {
        if (changePhrase == selectChangePhrase.First)
            firstPhraseCount = PhraseControlle(firstPhraseCount, firstPhrase, firstPhrases);
        if (changePhrase == selectChangePhrase.Second)
            secondPhraseCount = PhraseControlle(secondPhraseCount, secondPhrase, secondPhrases);

        alpha -= Time.deltaTime;
        alpha = Mathf.Max(0, alpha);

        if (interval >= 0.3f)
            frameScale.PlayBackwards();

        if (frame.rect.width == width)
            interval += Time.deltaTime;

        executionWord = $"{firstPhrases[firstPhraseCount].text} {secondPhrases[secondPhraseCount].text}";
    }

    private int PhraseControlle(int phraseCount, Transform phrase, Text[] phrases)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && phraseCount > 0)
        {
            phraseCount--;
            phrase.DOLocalMoveY(0.6f * phraseCount, 0.2f).SetEase(Ease.OutCubic);

            alpha = 0.5f;

            interval = 0;
            frameScale.PlayForward();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && phraseCount < phrases.Length - 1)
        {
            phraseCount++;
            phrase.DOLocalMoveY(0.6f * phraseCount, 0.2f).SetEase(Ease.OutCubic);

            alpha = 0.5f;

            interval = 0;
            frameScale.PlayForward();
        }

        for (int i = 0; i < phrases.Length; i++)
        {
            if (i == phraseCount - 1 || i == phraseCount + 1)
                phrases[i].color = new Color(0, 0, 0, alpha);
            else
                phrases[i].color = hiddenColor;

            if (i == phraseCount)
                phrases[i].color = displayColor;
        }

        return phraseCount;
    }

    public string ExecutionWord()
    {
        return executionWord;
    }

    private void DoTween()
    {
        var sequence = DOTween.Sequence();

        frameScale = sequence
            .Append(frame.DOSizeDelta(new Vector2(width, 60), 0.2f))
            .SetAutoKill(false)
            .Pause();
    }
}