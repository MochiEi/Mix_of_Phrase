using System.Collections.Generic;
using UnityEngine;

public class BoxMove : MonoBehaviour
{
    private Collider2D boxCollider;
    private Rigidbody2D rb;

    [SerializeField]
    private List<Collider2D> AddObj = new List<Collider2D>();

    [SerializeField]
    private float forceMultiplier = 1.0f;

    private void Start()
    {
        boxCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (boxCollider == null || rb == null) return;

        // OverlapColliderを使用してAddObjリストを更新
        boxCollider.OverlapCollider(new ContactFilter2D().NoFilter(), AddObj);

        if (AddObj.Count > 0)
        {
            ApplyPushForce();
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // 押されていなければX方向の速度を0にする
        }
    }

    private void ApplyPushForce()
    {
        Vector2 totalForce = Vector2.zero;

        foreach (Collider2D coll in AddObj)
        {
            if (coll.CompareTag("Pox") && coll.attachedRigidbody != null)
            {
                // 押しているオブジェクトの速度を取得
                Vector2 pushVelocity = coll.attachedRigidbody.velocity;
                Vector2 pushDirection = pushVelocity.normalized; // 押している方向（正規化）

                // ① 上下の押し（y ≠ 0）は無視
                if (Mathf.Abs(pushDirection.y) > 0.1f) continue;

                // ② Pox の足元が BoxMove の天井より上なら影響を受けない
                if (coll.bounds.min.y > boxCollider.bounds.max.y) continue;

                // ③ Pox の頭が BoxMove の床より下なら影響を受けない（下からの持ち上げを防ぐ）
                if (coll.bounds.max.y -0.2< boxCollider.bounds.min.y) continue;

                // ④ 横方向（x ≠ 0）なら適用
                if (Mathf.Abs(pushDirection.x) > 0.1f)
                {
                    totalForce += pushVelocity * forceMultiplier;
                }
            }
        }

        rb.velocity = new Vector2(totalForce.x, rb.velocity.y);
    }
}
