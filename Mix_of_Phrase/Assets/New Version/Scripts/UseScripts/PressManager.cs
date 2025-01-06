using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("Create Empty/BoxHitManager", 14)]
public class PressManager : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField]
    public List<bool> PushFlagList = new List<bool>();
    // Start is called before the first frame update

    private void Update()
    {
        if (gameObjects.Count >100)
        {
            gameObjects.RemoveAt(1);
        }
        if (PushFlagList.Count > 100)
        {
            PushFlagList.RemoveAt(1);
        }
    }

    public void HitSet(GameObject HitObj, bool Pushflag)//���������ۂɃq�b�g���X�g�𒲂�POX�^�O������ꍇPushflag��true��Ԃ�
    {
        //Debug.Log(HitObj);
        gameObjects.Add(HitObj);
        if (HitObj.tag == "Pox")
        {
            PushFlagList.Add(true);
        }
    }

    public void OutSet(GameObject OutObj, bool Pushflag)//���ꂽ�ۂɃq�b�g���X�g�𒲂ׁAPOX�^�O�������ꍇPushflag��false��Ԃ�
    {
        gameObjects.Add(OutObj);
        if (OutObj.tag == "Pox")
        {
            PushFlagList.Add(false);
        }
    }

}
