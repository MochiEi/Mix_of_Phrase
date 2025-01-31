using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;



public class ControllMaster : MonoBehaviour
{
    [SerializeField]
    string[] TagResarchFor;
    [SerializeField]
    Collider2D[] HitList;
    [SerializeField]
    List <Collider2D> InObject;

    [SerializeField]
    Vector2 Direction;
    [SerializeField]
    List<Vector2>DirectionList;

    Collider2D PlayerColl;
    GameObject Player;
    Rigidbody2D rb;

    [SerializeField]
    float DirectX, DirectY;

    // Start is called before the first frame update

    void Start()
    {
        Player = this.gameObject;
        PlayerColl = Player.gameObject.GetComponent<Collider2D>();
        rb = Player.GetComponent<Rigidbody2D>();

    }

    void Update()//Update�@���́E�A�j���[�V����
    {
        Hit(PlayerColl,TagResarchFor);
    }

    void Hit(Collider2D Hit, params string[] List)//�����������ɔ�����m�F����֐��B
    {
        Hit.OverlapCollider(new ContactFilter2D(),HitList);
        for (int i = 0; i < HitList.Length; i++)
        {
            if (HitList[i] != null)
            {
                Debug.Log(HitList[i].gameObject);
                if (InObject.Count > 10)
                {
                    InObject.RemoveAt(1);
                    DirectionList.RemoveAt(1);
                }
                DirectionList[i] = (HitList[i].ClosestPoint(transform.position) - (Vector2)transform.position).normalized;
                InObject.RemoveAll(x => x != null);
                if (DirectionList.Last().x >= DirectX && DirectionList.Last().x <= -DirectX && DirectionList.Last().x <= DirectY)
                {
                    Debug.Log("Set");
                    InObject.Add(HitList[i]);
                }
            }
            else
            {
                Debug.Log("Break");
            }
        }
        foreach (var ObjTag in InObject)
        {
            if (TargetTagResarch(ObjTag,List))
            {
                JumpFlgment(ObjTag.gameObject);
            }
            else
            {
                InObject.Remove(ObjTag);
            }
        }
        //���������I�u�W�F�N�g�̏����擾���z�񓙂Ɋi�[����
        //�z�񂩂�m�F���ĊY���̃I�u�W�F�N�g�̂�List�Ɉړ�����B
        //foreach�ŃI�u�W�F�N�g�̏�Ԃ��m�F�B
        //����O��TargetResarch�̊֐��������g�����肵���㕪��ŕ�����B
        //�����true��JumpFlagment�̊֐�����������B
        //Break
    }

    bool JumpFlgment(GameObject HitInObj)//�W�����v���ł����Ԃ��m�F����֐��B
    {
        Debug.Log("Reload");
        return true;
        //Hit�Œʉ߂����I�u�W�F�N�g���Q�Ƃ��A�n�ʂɖ��m�ɐG��Ă��邩�m�F����B
        //Direction�̐��l����ɂ��W�����v�̓�����P�B
        //�ǂƏ���F�m���₷���悤�ɑg�݂Ȃ����B
    }

    public static bool TargetTagResarch(Collider2D collder2d, params string[] tags)
    {
        return tags.Any(tag => collder2d.CompareTag(tag));
    }

}
