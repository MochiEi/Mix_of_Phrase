using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;

public class GoalGimmick01 : MonoBehaviour
{
    [SerializeField] Transform topBlock;
    [SerializeField] Transform bottomBlock;
    [SerializeField] Transform[] block;
    private Renderer[] blockRenderer = new Renderer[4];

    [SerializeField] Transform[] insert;
    private ActiveCheck[] trigger = new ActiveCheck[4];

    private Collider2D[] hitBox = new Collider2D[4];
    private Transform[] setBox = new Transform[4];
    [SerializeField] Transform[] goalBox;

    private Tween formerTween;
    private Tween laterTween;
    private int count;
    private bool isPlay;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            blockRenderer[i] = block[i].GetComponent<Renderer>();

            MonoBehaviour setTrigger = insert[i].GetComponent<MonoBehaviour>();
            trigger[i] = setTrigger as ActiveCheck;
            hitBox[i] = insert[i].GetComponent<Collider2D>();
        }

        DoTween();
    }

    void Update()
    {
        count = 0;

        for (int i = 0; i < 4; i++)
        {
            float posX = insert[i].position.x;
            float posY = blockRenderer[i].bounds.max.y - 0.2f;

            insert[i].position = new Vector2(posX, posY);
        }

        Overlap();

        foreach (ActiveCheck check in trigger)
        {
            if (check.IsActive())
                count++;
        }

        if (count == 4 && !isPlay)
        {
            isPlay = true;
            formerTween.PlayForward();
        }

        if (count < 4 && isPlay)
        {
            isPlay = false;

            if (formerTween.IsPlaying())
                formerTween.PlayBackwards();
            else
                laterTween.PlayBackwards();
        }
    }

    private void Overlap()
    {
        for (int i = 0; i < 4; i++)
        {
            Collider2D[] result = new Collider2D[10];
            int hitCount = hitBox[i].OverlapCollider(new ContactFilter2D(), result);

            if (hitCount <= 0) continue;

            foreach (Collider2D c in result)
            {
                if (c == null) continue;

                if (c.gameObject.CompareTag("Box"))
                {
                    setBox[i] = c.transform;
                    break;
                }
                else
                {
                    setBox[i] = null;
                }
            }
        }
    }

    private void DoTween()
    {
        var sequence = DOTween.Sequence();

        foreach (var w in block)
        {
            formerTween = sequence
                .Join(w.DOLocalMoveY(-2.3f, 2f)).SetEase(Ease.Linear)
                .Join(w.DOScaleY(2, 2f)).SetEase(Ease.Linear)
                .SetAutoKill(false)
                .Pause();
        }

        laterTween = sequence
            .Append(topBlock.DOScaleX(11, 2f)).SetEase(Ease.Linear)
            .Append(bottomBlock.DOScaleX(11, 2f)).SetEase(Ease.Linear)
            .AppendInterval(0.5f)
            .AppendCallback(() =>
            {
                for (int i = 0; i < 4; i++)
                {
                    setBox[i].GetComponent<SpriteRenderer>().enabled = false;
                    goalBox[i].gameObject.SetActive(true);
                    goalBox[i].position = setBox[i].position;
                }
            })
            .Append(bottomBlock.DOScaleX(2, 2f)).SetEase(Ease.Linear)
            .SetAutoKill(false)
            .Pause();
    }
}