using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class PressManager : MonoBehaviour
{
    
    public List<string> TagNames;
    [SerializeField]
    public List<bool> PushFlagList;

    public List<Collider2D> HitList;//箱たちが当たったオブジェクトの判定

    void RemoveAtList<T>(List<T> list)
    {
        if (list.Count > 10)
        {
            list.RemoveAt(0);
        }
    }

    public void Flagment(Collider2D coll)
    {
        coll.OverlapCollider(new ContactFilter2D().NoFilter(), HitList);
        TagNames.Clear();
        for (int i = 0; i < HitList.Count; i++)
        {
            TagNames.Add(HitList[i].tag);
        }
        if (TagNames.Contains("Pox"))
        {
            PushFlagList.Add(true);
        }
        else
        {
            PushFlagList.Add(false);
        }

        RemoveAtList(HitList);
        RemoveAtList(PushFlagList);
    }

}
