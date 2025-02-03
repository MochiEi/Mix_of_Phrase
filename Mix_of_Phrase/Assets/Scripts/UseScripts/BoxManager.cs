using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    Rigidbody2D rb2d;
    Collider2D boxCollider;
    //[SerializeField] Vector2 addForceNormalize;
    //[SerializeField] bool Push_Flag = false;

    //[SerializeField] GameObject SceneManager;
    //[SerializeField] PressManager pressManager;

    //[SerializeField]
    //private List<Collider2D> AddObj = new List<Collider2D>();

    //[SerializeField]
    //private float forceMultiplier = 1.0f;

    //Vector2 totalForce = Vector2.zero;

    // Start is called before the first frame update
    void Awake()
    {
        boxCollider = this.GetComponent<Collider2D>();
        rb2d = this.GetComponent<Rigidbody2D>();

        // SceneManagerやPressManagerが確実に取得できるかチェック
        //SceneManager = GameObject.Find("SceneManager");
        //if (SceneManager != null)
        //{
        //    pressManager = SceneManager.GetComponent<PressManager>();
        //}
        //else
        //{
        //    Debug.LogError("SceneManagerが見つかりません。");
        //}
    }

    private void Update()
    {
        //if (boxCollider == null || rb2d == null) return;

        //if (AddObj.Count > 0)
        //{
        //    ApplyPushForce();
        //}
        //else
        //{
        //    Debug.Log("ccc");
        //    pressManager.Flagment(boxCollider);
        //}

        //if (pressManager != null && pressManager.PushFlagList.Count > 0)
        //{
        //    // PushFlagListの状態を確認し、最後の状態に応じて処理
        //    if (pressManager.PushFlagList.Last() == true)
        //    {
        //        Debug.Log("AAA");
        //    }
        //}
        //else
        //{
        //    Debug.LogWarning("PressFlagListが空か、PressManagerが設定されていません。");
        //}
    }

    //private void ApplyPushForce()
    //{
    //    totalForce = Vector2.zero;

    //    foreach (Collider2D coll in AddObj)
    //    {
    //        if (coll.CompareTag("Pox") && coll.attachedRigidbody != null)
    //        {
    //            // 押しているオブジェクトの速度を取得
    //            Vector2 pushVelocity = coll.attachedRigidbody.velocity;
    //            Vector2 pushDirection = pushVelocity.normalized; // 押している方向（正規化）

    //            // ① 上下の押し（y ≠ 0）は無視
    //            if (Mathf.Abs(pushDirection.y) > 0.1f) continue;

    //            // ② Pox の足元が BoxMove の天井より上なら影響を受けない
    //            if (coll.bounds.min.y > boxCollider.bounds.max.y) continue;

    //            // ③ Pox の頭が BoxMove の床より下なら影響を受けない（下からの持ち上げを防ぐ）
    //            if (coll.bounds.max.y - 0.2 < boxCollider.bounds.min.y) continue;

    //            // ④ 横方向（x ≠ 0）なら適用
    //            if (Mathf.Abs(pushDirection.x) > 0.1f)
    //            {
    //                pressManager.Flagment(boxCollider);
    //            }
    //        }
    //    }
    //}

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("BBB");
        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
    }
}

