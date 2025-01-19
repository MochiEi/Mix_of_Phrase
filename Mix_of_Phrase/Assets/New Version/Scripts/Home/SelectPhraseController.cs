using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectPhraseController : MonoBehaviour
{
    private int phraseCount;
    private string firstPhrase;
    private string secondPhrase;

    [Header("フレーズ")]
    [SerializeField] Text selectPhrase;

    [Header("ボタン")]
    [SerializeField] Transform buttons;
    private Text[] buttonText;

    void Start()
    {
        buttonText = buttons.Cast<Transform>().Select(button => button.GetChild(0).GetComponent<Text>()).ToArray();

        phraseCount = 0;
        firstPhrase = "click";
        secondPhrase = "phrase";
    }

    void Update()
    {
        selectPhrase.text = $"{firstPhrase} {secondPhrase}";
    }

    public void Button01()
    {
        if (phraseCount == 0)
        {
            phraseCount++;
            firstPhrase = buttonText[0].text;
            secondPhrase = "";
            return;
        }
        if (phraseCount == 1)
        {
            phraseCount++;
            secondPhrase = buttonText[0].text;
            return;
        }
    }

    public void Button02()
    {
        if (phraseCount == 0)
        {
            phraseCount++;
            firstPhrase = buttonText[1].text;
            secondPhrase = "";
            return;
        }
        if (phraseCount == 1)
        {
            phraseCount++;
            secondPhrase = buttonText[1].text;
            return;
        }
    }

    public void Button03()
    {
        if (phraseCount == 0)
        {
            phraseCount++;
            firstPhrase = buttonText[2].text;
            secondPhrase = "";
            return;
        }
        if (phraseCount == 1)
        {
            phraseCount++;
            secondPhrase = buttonText[2].text;
            return;
        }
    }

    public void PhraseErase()
    {
        phraseCount = 0;
        firstPhrase = "click";
        secondPhrase = "phrase";
    }
}