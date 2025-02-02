using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingGimmickController02 : MonoBehaviour
{
    private Transform wall;
    private Renderer wallRenderer;
    private float anchor;

    private Collider2D activeArea;
    private Collider2D[] result = new Collider2D[3];
    private ContactFilter2D filter = new ContactFilter2D();

    private Tween push;

    void Start()
    {
        wall = transform.GetChild(0);
        activeArea = transform.GetChild(1).GetComponent<Collider2D>();

        wallRenderer = wall.GetComponent<Renderer>();
        anchor = wallRenderer.bounds.max.x;

        filter.useLayerMask = true;
        filter.SetLayerMask(1 << 7);

        Dotweem();
    }

    void Update()
    {
        Vector3 pos = wall.position;
        float targetPos = wallRenderer.bounds.max.x;

        if (targetPos != anchor)
        {
            pos.x = anchor - wall.localScale.x / 2;
            wall.position = pos;
        }

        Overlap();
    }

    private void Overlap()
    {
        int count = activeArea.OverlapCollider(filter, result);

        if (count > 0)
            push.PlayForward();
        else
            push.PlayBackwards();
    }

    private void Dotweem()
    {
        push = DOTween.Sequence()
            .Append(wall.DOScaleX(22f, 2f).SetEase(Ease.OutSine))
            .SetAutoKill(false).Pause();
    }
}