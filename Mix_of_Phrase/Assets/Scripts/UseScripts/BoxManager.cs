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

        // SceneManager��PressManager���m���Ɏ擾�ł��邩�`�F�b�N
        //SceneManager = GameObject.Find("SceneManager");
        //if (SceneManager != null)
        //{
        //    pressManager = SceneManager.GetComponent<PressManager>();
        //}
        //else
        //{
        //    Debug.LogError("SceneManager��������܂���B");
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
        //    // PushFlagList�̏�Ԃ��m�F���A�Ō�̏�Ԃɉ����ď���
        //    if (pressManager.PushFlagList.Last() == true)
        //    {
        //        Debug.Log("AAA");
        //    }
        //}
        //else
        //{
        //    Debug.LogWarning("PressFlagList���󂩁APressManager���ݒ肳��Ă��܂���B");
        //}
    }

    //private void ApplyPushForce()
    //{
    //    totalForce = Vector2.zero;

    //    foreach (Collider2D coll in AddObj)
    //    {
    //        if (coll.CompareTag("Pox") && coll.attachedRigidbody != null)
    //        {
    //            // �����Ă���I�u�W�F�N�g�̑��x���擾
    //            Vector2 pushVelocity = coll.attachedRigidbody.velocity;
    //            Vector2 pushDirection = pushVelocity.normalized; // �����Ă�������i���K���j

    //            // �@ �㉺�̉����iy �� 0�j�͖���
    //            if (Mathf.Abs(pushDirection.y) > 0.1f) continue;

    //            // �A Pox �̑����� BoxMove �̓V�����Ȃ�e�����󂯂Ȃ�
    //            if (coll.bounds.min.y > boxCollider.bounds.max.y) continue;

    //            // �B Pox �̓��� BoxMove �̏���艺�Ȃ�e�����󂯂Ȃ��i������̎����グ��h���j
    //            if (coll.bounds.max.y - 0.2 < boxCollider.bounds.min.y) continue;

    //            // �C �������ix �� 0�j�Ȃ�K�p
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

