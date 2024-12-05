using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;

public class GoalGimmick01 : MonoBehaviour
{
    private ActiveCheck[] trigger = new ActiveCheck[4];
    private Transform[] wall = new Transform[4];

    private Tween tween;
    private int count;
    private bool isPlay;

    void Start()
    {
        int triggerCount = 0;
        int wallCount = 0;

        foreach (Transform obj in transform)
        {
            GameObject child = obj.gameObject;

            if (child.CompareTag("InsertGimmick"))
            {
                MonoBehaviour setTrigger = child.GetComponent<MonoBehaviour>();
                trigger[triggerCount] = setTrigger as ActiveCheck;
                triggerCount++;
            }
            if (child.CompareTag("Ground"))
                wall[wallCount++] = child.transform;
        }

        DoTween();
    }

    void Update()
    {
        count = 0;

        foreach (ActiveCheck check in trigger)
        {
            if (check.IsActive())
                count++;
        }

        if (count == 4 && !isPlay)
        {
            isPlay = true;
            tween.PlayForward();
        }

        if (count < 4 && isPlay)
        {
            isPlay = false;
            tween.PlayBackwards();
        }
    }

    private void DoTween()
    {
        var sequence = DOTween.Sequence();

        foreach (var w in wall)
        {
            tween = sequence
                .Join(w.DOLocalMoveY(2.7f, 2f))
                .Join(w.DOScaleY(2, 2f))
                .SetAutoKill(false)
                .Pause();
        }
    }
}