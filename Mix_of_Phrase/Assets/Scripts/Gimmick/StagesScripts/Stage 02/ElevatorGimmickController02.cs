using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorGimmickController02 : MonoBehaviour
{
    private Transform elevator;
    private Renderer elevaterRenderer;
    private float anchor;

    private Transform triggerObj;
    private ActiveCheck trigger;

    private Tween moveElevaator;

    void Start()
    {
        elevator = transform.GetChild(0);
        triggerObj = transform.GetChild(1);

        elevaterRenderer = elevator.GetComponent<Renderer>();
        anchor = elevaterRenderer.bounds.min.y;

        trigger = triggerObj.GetComponent<MonoBehaviour>() as ActiveCheck;

        Dotween();
    }

    void Update()
    {
        Vector3 pos = elevator.position;
        float bottomPos = elevaterRenderer.bounds.min.y;

        if (bottomPos != anchor)
        {
            pos.y = anchor + elevator.localScale.y / 2;
            elevator.position = pos;

            pos.y = elevaterRenderer.bounds.max.y - 0.2f;
            triggerObj.position = pos;
        }

        if (trigger.IsActive())
            moveElevaator.PlayForward();
        else
            moveElevaator.PlayBackwards();
    }

    private void Dotween()
    {
        moveElevaator = DOTween.Sequence()
            .Append(elevator.DOScaleY(8f, 1.5f).SetEase(Ease.OutSine))
            .SetAutoKill(false).Pause();
    }
}