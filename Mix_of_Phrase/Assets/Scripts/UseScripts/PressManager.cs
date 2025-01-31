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
        // ���X�g��10�𒴂��Ȃ��悤�ɐ���
        if (gameObjects.Count > 10)
        {
            gameObjects.RemoveAt(0); // �C���f�b�N�X0�̗v�f���폜
            PushFlagList.RemoveAt(0); // �C���f�b�N�X0��PushFlag���폜
        }
    }

    // �q�b�g�����I�u�W�F�N�g�����X�g�ɒǉ����APushFlag��ݒ肷��
    public void HitSet(GameObject HitObj)
    {
        // ���łɑ��݂���ꍇ�͉������Ȃ�
        if (!gameObjects.Contains(HitObj))
        {
            gameObjects.Add(HitObj);

            // Pox�^�O�̃I�u�W�F�N�g���ǉ����ꂽ��PushFlag��true��
            if (HitObj.CompareTag("Pox"))
            {
                PushFlagList.Add(true);
            }
            else
            {
                PushFlagList.Add(false); // ����ȊO�̃^�O��false
            }
        }
    }

    // ���ꂽ�I�u�W�F�N�g�����X�g����폜���APushFlag���X�V
    public void OutSet(GameObject OutObj)
    {
        int index = gameObjects.IndexOf(OutObj);
        if (index != -1)
        {
            // ���ł�PushFlagList�ɓ�����Ԃ�false������ꍇ�͉������Ȃ�
            if (PushFlagList[index] != false)
            {
                PushFlagList[index] = false;  // ���ꂽ�ۂ�false�ɐݒ�
            }
        }
    }

    // �I�u�W�F�N�g�̏d�Ȃ�����o����PushFlag��ݒ�
    public void Flagment(Collider2D col)
    {
        int count = col.OverlapCollider(new ContactFilter2D(), Addobj);

        if (count > 0)
        {
            for (int i = 0; i < count; i++) // �C��: i <= count -> i < count
            {
                if (TargetTagResarch(Addobj[i], tagList))  // �^�O���`�F�b�N
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

    // �^�O�𒲂ׂ郁�\�b�h
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
