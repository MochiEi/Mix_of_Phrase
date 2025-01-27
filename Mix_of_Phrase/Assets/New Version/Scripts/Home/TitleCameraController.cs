using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraController : MonoBehaviour
{
    private Transform cam;
    private TitleCameraController camController;

    [SerializeField] Transform systemController;
    private TitlePhraseActionContllore contllore;

    [SerializeField] Transform selectPhrase;
    private SelectPhraseController phraseController;

    private Tween toSelect;
    private bool select;

    void Start()
    {
        cam = transform;
        camController = cam.GetComponent<TitleCameraController>();
        contllore = systemController.GetComponent<TitlePhraseActionContllore>();
        phraseController = selectPhrase.GetComponent<SelectPhraseController>();

        var sequence = DOTween.Sequence();
        toSelect = sequence
            .AppendCallback(() =>
            {
                if (select)
                {
                    camController.enabled = false;
                    contllore.enabled = true;
                }

                select = false;
            })
            .Append(cam.DOMoveY(-10.0f, 2f).SetEase(Ease.InOutSine))
            .AppendCallback(() =>
            {
                phraseController.PhraseErase();
                select = true;
            })
            .SetAutoKill(false);
    }

    void Update()
    {
        if (!select)
            toSelect.PlayForward();
    }

    public void toTitle()
    {
        toSelect.PlayBackwards();
    }


}
