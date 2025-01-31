using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressManager : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField]
    string[] tagList;
    [SerializeField]
    List<Collider2D> Addobj;
    [SerializeField]
    public List<bool> PushFlagList = new List<bool>();

    private void Update()
    {
        // リストが10を超えないように制限
        if (gameObjects.Count > 10)
        {
            gameObjects.RemoveAt(0); // インデックス0の要素を削除
            PushFlagList.RemoveAt(0); // インデックス0のPushFlagも削除
        }
    }

    // ヒットしたオブジェクトをリストに追加し、PushFlagを設定する
    public void HitSet(GameObject HitObj)
    {
        // すでに存在する場合は何もしない
        if (!gameObjects.Contains(HitObj))
        {
            gameObjects.Add(HitObj);

            // Poxタグのオブジェクトが追加されたらPushFlagをtrueに
            if (HitObj.CompareTag("Pox"))
            {
                PushFlagList.Add(true);
            }
            else
            {
                PushFlagList.Add(false); // それ以外のタグはfalse
            }
        }
    }

    // 離れたオブジェクトをリストから削除し、PushFlagを更新
    public void OutSet(GameObject OutObj)
    {
        int index = gameObjects.IndexOf(OutObj);
        if (index != -1)
        {
            // すでにPushFlagListに同じ状態のfalseがある場合は何もしない
            if (PushFlagList[index] != false)
            {
                PushFlagList[index] = false;  // 離れた際にfalseに設定
            }
        }
    }

    // オブジェクトの重なりを検出してPushFlagを設定
    public void Flagment(Collider2D col)
    {
        int count = col.OverlapCollider(new ContactFilter2D(), Addobj);

        if (count > 0)
        {
            for (int i = 0; i < count; i++) // 修正: i <= count -> i < count
            {
                if (TargetTagResarch(Addobj[i], tagList))  // タグをチェック
                {
                    HitSet(Addobj[i].gameObject);
                }
                else
                {
                    OutSet(Addobj[i].gameObject);
                }
            }
        }
    }

    // タグを調べるメソッド
    private bool TargetTagResarch(Collider2D coll, string[] tagList)
    {
        foreach (string tag in tagList)
        {
            if (coll.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }
}
