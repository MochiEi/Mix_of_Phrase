using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePhraseController : MonoBehaviour
{
    [SerializeField] Text phraseBoardText;

    private int textCount;
    private string firstText, secondText;
        
    void Start()
    {
        firstText = "";
        secondText = "";
    }

    void Update()
    {
        phraseBoardText.text = firstText + " " + secondText;
    }

    public void buttom_Game()
    {
        if(textCount == 0)
        {
            firstText = "game";
            textCount++ ;
        }
        if(textCount == 1)
        {
            secondText = "game";
            textCount++;
        }
    }
}
