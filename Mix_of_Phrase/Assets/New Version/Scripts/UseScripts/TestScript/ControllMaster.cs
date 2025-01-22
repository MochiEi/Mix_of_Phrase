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

    void Update()//Update　入力・アニメーション
    {
        Hit(PlayerColl,TagResarchFor);
    }

    void Hit(Collider2D Hit, params string[] List)//当たった時に判定を確認する関数。
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
        //当たったオブジェクトの情報を取得し配列等に格納する
        //配列から確認して該当のオブジェクトのみListに移動する。
        //foreachでオブジェクトの状態を確認。
        //分岐前にTargetResarchの関数処理を使い判定した後分岐で分ける。
        //分岐のtrueにJumpFlagmentの関数処理を入れる。
        //Break
    }

    bool JumpFlgment(GameObject HitInObj)//ジャンプができる状態か確認する関数。
    {
        Debug.Log("Reload");
        return true;
        //Hitで通過したオブジェクトを参照し、地面に明確に触れているか確認する。
        //Directionの数値制御によるジャンプの動作改善。
        //壁と床を認知しやすいように組みなおす。
    }

    public static bool TargetTagResarch(Collider2D collder2d, params string[] tags)
    {
        return tags.Any(tag => collder2d.CompareTag(tag));
    }

}
