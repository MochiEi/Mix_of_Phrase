using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PhraseController : MonoBehaviour
{
    [SerializeField] Transform pox;

    private Transform firstPhrase;
    private Transform secondPhrase;
    private Transform phraseStorage;
    private Text constPhrase;
    private List<Text> phrases = new List<Text>();
    private List<Text> phraseColor = new List<Text>();

    [SerializeField] PhraseToBeChanged phraseToBeChanged;
    private enum PhraseToBeChanged
    {
        first, second
    };

    private int select;
    private float alpha;
    private bool isActive;

    private string executionWord;

    void Start()
    {
        firstPhrase = GameObject.Find("FirstPhrase").transform;
        secondPhrase = GameObject.Find("SecondPhrase").transform;

        if (phraseToBeChanged == PhraseToBeChanged.first)
        {
            phraseStorage = firstPhrase;

            constPhrase = secondPhrase.GetChild(0).GetComponent<Text>();

            foreach (Transform phrase in firstPhrase)
                phrases.Add(phrase.GetComponent<Text>());
        }

        if (phraseToBeChanged == PhraseToBeChanged.second)
        {
            phraseStorage = secondPhrase;

            constPhrase = firstPhrase.GetChild(0).GetComponent<Text>();

            foreach (Transform phrase in secondPhrase)
                phrases.Add(phrase.GetComponent<Text>());
        }

        foreach (Transform phrase in firstPhrase)
            phraseColor.Add(phrase.GetComponent<Text>());

        foreach (Transform phrase in secondPhrase)
            phraseColor.Add(phrase.GetComponent<Text>());

        alpha = 0;
    }

    void Update()
    {
        transform.position = pox.position;

        ChangePhrase();

        for (int i = 0; i < phrases.Count; i++)
        {
            if (i == select)
            {
                phrases[i].color = Color.black;

                if (phraseToBeChanged == PhraseToBeChanged.first)
                    executionWord = $"{phrases[i].text} {constPhrase.text}";

                if (phraseToBeChanged == PhraseToBeChanged.second)
                    executionWord = $"{constPhrase.text} {phrases[i].text}";
            }
            else
            {
                phrases[i].color = Color.gray;
            }
        }

        alpha -= Time.deltaTime;
        alpha = Mathf.Max(0, alpha);

        foreach (Text phrase in phraseColor)
        {
            Color setColor = phrase.color;

            phrase.color = new Color(setColor.r, setColor.g, setColor.b, alpha);
        }

        for (int i = 0; i < phrases.Count; i++)
        {
            Color setColor = phrases[i].color;

            if (Mathf.Abs(select - i) >= 2)
                phrases[i].color = new Color(setColor.r, setColor.g, setColor.b, 0);
        }
    }

    private void ChangePhrase()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            select--;
            alpha = 2;
            isActive = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            select++;
            alpha = 2;
            isActive = true;
        }
        select = Mathf.Clamp(select, 0, phrases.Count - 1);

        if (isActive)
        {
            isActive = false;
            phraseStorage.DOLocalMoveY(0.4f * select, 0.5f).SetEase(Ease.OutSine);
        }
    }

    public string ExecutionWord()
    {
        return executionWord;
    }
}
