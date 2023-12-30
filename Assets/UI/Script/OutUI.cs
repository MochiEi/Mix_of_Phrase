using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class OutUI : MonoBehaviour
{
    //メニューの存在状態
    [HideInInspector]
    public bool MenuWhereabouts = false;

    [SerializeField, Header("移動する位置")]
    private float Left;
    [SerializeField]
    private float Right;

    Tween tweenerCore = null;


    [SerializeField, Header("移動時間")]
    private float EasingMaxTimer;

    //移動用の座標
    RectTransform rectTransform;

    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
    }

    void Update()
    {
        Debug.Log(rectTransform.position);
        //メニューの出し入れ
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MenuWhereabouts = !MenuWhereabouts;
            if (!MenuWhereabouts)
            {
                if (tweenerCore != null) tweenerCore.Kill();
                tweenerCore = rectTransform.DOAnchorPosX(Left, EasingMaxTimer).SetEase(Ease.OutCubic);

                //transform.DOLocalMove(new Vector3( Left,50,0), EasingMaxTimer).SetEase(Ease.OutCubic);
            }
            else
            {
                if (tweenerCore != null) tweenerCore.Kill();
                tweenerCore = rectTransform.DOAnchorPosX(Right, EasingMaxTimer).SetEase(Ease.OutCubic);

                //transform.DOLocalMove(new Vector3(Right, 50, 0), EasingMaxTimer).SetEase(Ease.OutCubic);
            }
        }
    }


}
