using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class PhraseController : MonoBehaviour
{
    [SerializeField] selectChangePhrase changePhrase;
    private enum selectChangePhrase
    {
        First, Second
    };

    //フレーズの取得、保持、及び出力の変数
    private Transform firstPhrase;
    private Transform secondPhrase;
    [SerializeField] string[] firstPhrases;
    [SerializeField] string[] secondPhrases;
    private string executionWord;

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "FirstPhrase")
                firstPhrase = child;
            if (child.gameObject.name == "SecondPhrase")
                secondPhrase = child;
        }

        firstPhrases = firstPhrase.Cast<Transform>().Select(phrase => phrase.GetComponent<Text>().text).ToArray();
        secondPhrases = secondPhrase.Cast<Transform>().Select(phrase => phrase.GetComponent<Text>().text).ToArray();
    }

    void Update()
    {

    }

    public string ExecutionWord()
    {
        return executionWord;
    }
}