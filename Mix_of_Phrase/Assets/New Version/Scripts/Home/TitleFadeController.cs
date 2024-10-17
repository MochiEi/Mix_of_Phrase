using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleFadeController : MonoBehaviour
{
    [SerializeField] Image bluckFade;
    private FadeController fadeController;
    private bool isActivBluck;

    [SerializeField] GameObject whiteFade;
    private bool isActivWhite;


    void Start()
    {
        fadeController = bluckFade.GetComponent<FadeController>();

        whiteFade.SetActive(true);
        isActivWhite = true;
    }

    void Update()
    {
        
    }
}
