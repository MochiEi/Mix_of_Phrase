using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDateController : MonoBehaviour
{   
    public static GameDateController Instance { get; private set; }

    void Awake()
    {
        // �C���X�^���X�����łɑ��݂��Ă�����j������
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // �C���X�^���X��ݒ肵�Ĕj������Ȃ��悤�ɂ���
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // �Z�[�u����
    public void Save(int stageNumber)
    {
        PlayerPrefs.SetInt("ClearedStage", stageNumber);
        PlayerPrefs.Save();
    }

    // ���[�h����
    public int Load()
    {
        return PlayerPrefs.GetInt("ClearedStage", 0);
    }

    // �f���[�g����
    public void Delete()
    {
        PlayerPrefs.SetInt("ClearedStage", 0);
        PlayerPrefs.Save();
    }
}