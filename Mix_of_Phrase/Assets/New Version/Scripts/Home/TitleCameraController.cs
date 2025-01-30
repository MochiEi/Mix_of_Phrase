using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraController : MonoBehaviour
{
    [SerializeField] Transform phraseBoard;
    private SelectPhraseController controller;

    void Awake()
    {
        controller = phraseBoard.GetComponent<SelectPhraseController>();

        float posY = 0;
        posY = IsSelect.isSelected ? -10 : 0;
        transform.position = new Vector3(0, posY, -10);
    }

    void Update()
    {
        if (IsSelect.isActive)
        {
            IsSelect.isActive = false;

            if (transform.position.y == 0)
            {
                DOTween.Sequence()
                    .Append(transform.DOMoveY(-10, 1f)).SetEase(Ease.InOutSine)
                    .AppendCallback(controller.PhraseErase);
            }
        }
    }

    public void ToTitle()
    {
        transform.DOMoveY(0, 1f).SetEase(Ease.InOutSine);
    }
}