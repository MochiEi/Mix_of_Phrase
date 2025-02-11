using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GoalGimmickController02 : MonoBehaviour
{
    [Header("開始地点")]
    [SerializeField] Transform startGround;
    private bool isStart = false;
    private Tween groundColl;

    [Header("ゴール")]
    [SerializeField] CanvasGroup goalGroup;
    [SerializeField] MonoBehaviour goalButton;
    private ActiveCheck trigger;
    private bool isActive;

    [System.Serializable]
    private struct Goal
    {
        public GameObject text;
        public Transform button;
        public SpriteRenderer lanp;
        public bool isActive;
        public ActiveCheck trigger;
    }
    [SerializeField] Goal[] goal;

    //--------------- DOTween用変数 ---------------//

    private Tween goalCheck;
    private Tween stageClear;
    [SerializeField] float fadeSpeed;

    Collider2D[] hitColl = new Collider2D[3];
    ContactFilter2D filter = new ContactFilter2D();

    void Start()
    {
        filter.useLayerMask = true;
        filter.SetLayerMask(1 << 7);

        GoalTextlighting();

        trigger = goalButton as ActiveCheck;

        for (int i = 0; i < goal.Length; i++)
            goal[i].trigger = goal[i].button.GetComponent<MonoBehaviour>() as ActiveCheck;
    }

    void Update()
    {
        if (startGround.gameObject.activeSelf)
            StartGroundControlle();

        bool[] triggerCheck = goal.Select(g => g.isActive).ToArray();
        for (int i = 0; i < goal.Length; i++)
        {
            if (goal[i].trigger.IsActive())
                goal[i].isActive = true;

            if (!triggerCheck[i] && goal[i].isActive)
            {
                goal[i].lanp.DOColor(new Color(1, 1, 1), 0.5f);
                goal[i].text.SetActive(true);
            }
        }

        if(isActive && !goalCheck.IsPlaying() && goal.All(g => g.isActive))
        {
            stageClear.Play();
        }

        if (trigger.IsActive() && !isActive && !goalCheck.IsPlaying())
        {
            isActive = true;
            goalCheck.Restart();
        }
        else if (!trigger.IsActive() && !goalCheck.IsPlaying() && !stageClear.IsPlaying()) 
        {
            isActive = false;
        }
    }

    void GoalTextlighting()
    {
        goalCheck = DOTween.Sequence()
            .Append(goalGroup.DOFade(1, fadeSpeed))
            .Append(goalGroup.DOFade(0, fadeSpeed))
            .SetLoops(3)
            .SetAutoKill(false).Pause();

        stageClear = DOTween.Sequence()
            .AppendInterval(0.5f)
            .Append(goalGroup.DOFade(1, 3f))
            .AppendInterval(2f)
            .AppendCallback(() => { GameController.Instance.SaveClearStage(3); GameController.Instance.ToHome(); })
            .SetAutoKill(false).Pause();
    }

    void StartGroundControlle()
    {
        Collider2D ground = startGround.GetComponent<Collider2D>();

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