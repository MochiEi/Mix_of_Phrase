using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitListBox : MonoBehaviour
{
    // 自作classはSerializableする
    [System.Serializable]
    public class HitListManager
    {
        public GameObject HitObj; // 
        public bool press;        // 
    }
    // 通常のListとしてinspectorで扱える
    [SerializeField]
    List<HitListManager> hitList;
    [SerializeField]
    List<GameObject> ObjManager = new List<GameObject>();

    void PressFlagManager(bool Press, GameObject obj)
    {
        bool flag = ObjManager.Any(Item => Item.tag == obj.tag);
    }
}
