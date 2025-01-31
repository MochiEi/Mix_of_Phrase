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

        // OverlapCollider���g�p����AddObj���X�g���X�V
        boxCollider.OverlapCollider(new ContactFilter2D().NoFilter(), AddObj);

        if (AddObj.Count > 0)
        {
            ApplyPushForce();
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // ������Ă��Ȃ����X�����̑��x��0�ɂ���
        }
    }

    private void ApplyPushForce()
    {
        Vector2 totalForce = Vector2.zero;

        foreach (Collider2D coll in AddObj)
        {
            if (coll.CompareTag("Pox") && coll.attachedRigidbody != null)
            {
                // �����Ă���I�u�W�F�N�g�̑��x���擾
                Vector2 pushVelocity = coll.attachedRigidbody.velocity;
                Vector2 pushDirection = pushVelocity.normalized; // �����Ă�������i���K���j

                // �@ �㉺�̉����iy �� 0�j�͖���
                if (Mathf.Abs(pushDirection.y) > 0.1f) continue;

                // �A Pox �̑����� BoxMove �̓V�����Ȃ�e�����󂯂Ȃ�
                if (coll.bounds.min.y > boxCollider.bounds.max.y) continue;

                // �B Pox �̓��� BoxMove �̏���艺�Ȃ�e�����󂯂Ȃ��i������̎����グ��h���j
                if (coll.bounds.max.y -0.2< boxCollider.bounds.min.y) continue;

                // �C �������ix �� 0�j�Ȃ�K�p
                if (Mathf.Abs(pushDirection.x) > 0.1f)
                {
                    totalForce += pushVelocity * forceMultiplier;
                }
            }
        }

        rb.velocity = new Vector2(totalForce.x, rb.velocity.y);
    }
}
