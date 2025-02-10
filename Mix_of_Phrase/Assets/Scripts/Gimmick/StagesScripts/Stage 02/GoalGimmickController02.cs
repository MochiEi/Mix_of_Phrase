using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalGimmickController02 : MonoBehaviour
{
    [Header("開始地点")]
    [SerializeField] Transform startGround;
    private bool isStart = false;
    private Tween groundColl;

    [Header("ゴール")]
    [SerializeField] CanvasGroup goal;
    private Tween fade;

    void Start()
    {
        GoalTextlighting();
    }

    void Update()
    {
        if (startGround.gameObject.activeSelf)
            StartGroundControlle();

        if (!fade.IsPlaying())
            fade.Restart();
    }

    void GoalTextlighting()
    {
        fade = DOTween.Sequence()
            .Append(goal.DOFade(1, 1.5f))
            .Append(goal.DOFade(0, 1.5f))
            .SetAutoKill(false).Pause();
    }

    void StartGroundControlle()
    {
        Collider2D ground = startGround.GetComponent<Collider2D>();

        Collider2D[] hitColl = new Collider2D[3];
        ContactFilter2D filter = new ContactFilter2D();

        filter.useLayerMask = true;
        filter.SetLayerMask(1 << 7);

        int count = ground.OverlapCollider(filter, hitColl);

        if (count > 0 && !isStart)
        {
            isStart = true;
            groundColl = DOTween.Sequence()
                .Append(startGround.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f))
                .AppendCallback(() => startGround.gameObject.SetActive(false))
                .SetAutoKill(true).Pause();
        }

        if (count == 0 && isStart)
        {
            groundColl.Play();
        }
    }
}