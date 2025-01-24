using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    private float alpha;
    private Color displayColor;
    private Color hiddenColor;

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "FirstPhrase")
                firstPhrase = child;
            if (child.gameObject.name == "SecondPhrase")
                secondPhrase = child;
        }

        firstPhrases = firstPhrase.Cast<Transform>().Select(phrase => phrase.GetComponent<Text>()).ToArray();
        secondPhrases = secondPhrase.Cast<Transform>().Select(phrase => phrase.GetComponent<Text>()).ToArray();

        firstPhraseCount = 0;
        secondPhraseCount = 0;

        alpha = 0;
        displayColor = new Color(0, 0, 0, 1.0f);
        hiddenColor = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        if (changePhrase == selectChangePhrase.First)
            FirstPhraseControlle();
        if (changePhrase == selectChangePhrase.Second)
            SecondPhraseControlle();

    }

    private void FirstPhraseControlle()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && firstPhraseCount > 0)
        {
            firstPhraseCount--;
            firstPhrase.DOLocalMoveY(0.6f * firstPhraseCount, 0.2f).SetEase(Ease.OutCubic);

            alpha = 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && firstPhraseCount < firstPhrases.Length - 1)
        {
            firstPhraseCount++;
            firstPhrase.DOLocalMoveY(0.6f * firstPhraseCount, 0.2f).SetEase(Ease.OutCubic);

            alpha = 0.5f;
        }

        for (int i = 0; i < firstPhrases.Length; i++)
        {
            if (i == firstPhraseCount - 1 || i == firstPhraseCount + 1)
                firstPhrases[i].color = new Color(0, 0, 0, alpha);
            else
                firstPhrases[i].color = hiddenColor;

            if (i == firstPhraseCount)
                firstPhrases[i].color = displayColor;            
        }

        alpha -= Time.deltaTime;
    }

    private void SecondPhraseControlle()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && secondPhraseCount > 0)
        {
            secondPhraseCount--;
            secondPhrase.DOLocalMoveY(0.6f * secondPhraseCount, 0.2f).SetEase(Ease.OutCubic);

            alpha = 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && secondPhraseCount < secondPhrases.Length - 1)
        {
            secondPhraseCount++;
            secondPhrase.DOLocalMoveY(0.6f * secondPhraseCount, 0.2f).SetEase(Ease.OutCubic);

            alpha = 0.5f;
        }

        for (int i = 0; i < secondPhrases.Length; i++)
        {
            if (i == secondPhraseCount - 1 || i == secondPhraseCount + 1)
                secondPhrases[i].color = new Color(0, 0, 0, alpha);
            else
                secondPhrases[i].color = hiddenColor;

            if (i == secondPhraseCount)
                secondPhrases[i].color = displayColor;            
        }

        alpha -= Time.deltaTime;
    }

    public string ExecutionWord()
    {
        return executionWord;
    }
}