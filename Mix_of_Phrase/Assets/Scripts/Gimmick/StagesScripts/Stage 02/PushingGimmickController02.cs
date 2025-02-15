using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PushingGimmickController02 : MonoBehaviour
{
    [SerializeField] Transform wall;
    private Renderer wallRenderer;
    private float anchor;

    [SerializeField] Collider2D activeArea;
    private Collider2D[] result = new Collider2D[3];
    private ContactFilter2D filter = new ContactFilter2D();

    private enum RightLeft { Left, Right };
    [SerializeField] RightLeft rightLeft;

    [SerializeField] float stretchinglength;

    private Tween push;

    void Start()
    {
        wallRenderer = wall.GetComponent<Renderer>();

        if (rightLeft == RightLeft.Left)
            anchor = wallRenderer.bounds.min.x;
        else
            anchor = wallRenderer.bounds.max.x;

        filter.useLayerMask = true;
        filter.SetLayerMask(1 << 7);

        Dotweem();
    }

    void Update()
    {
        Vector3 pos = wall.position;
        float targetPos;

        if (rightLeft == RightLeft.Left)
            targetPos = wallRenderer.bounds.min.x;
        else
            targetPos = wallRenderer.bounds.max.x;

        Overlap();

        if (targetPos != anchor)
        {
            if (rightLeft == RightLeft.Left)
                pos.x = anchor + wall.localScale.x / 2;
            else
                pos.x = anchor - wall.localScale.x / 2;

            wall.position = pos;
        }
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
            .Append(wall.DOScaleX(stretchinglength, 2f).SetEase(Ease.OutSine))
            .SetAutoKill(false).Pause();
    }
}