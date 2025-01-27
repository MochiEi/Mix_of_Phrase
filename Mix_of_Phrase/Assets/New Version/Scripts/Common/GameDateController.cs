using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDateController : MonoBehaviour
{   
    public static GameDateController Instance { get; private set; }

    void Awake()
    {
        // インスタンスがすでに存在していたら破棄する
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // インスタンスを設定して破棄されないようにする
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // セーブ処理
    public void Save(int stageNumber)
    {
        PlayerPrefs.SetInt("ClearedStage", stageNumber);
        PlayerPrefs.Save();
    }

    // ロード処理
    public int Load()
    {
        return PlayerPrefs.GetInt("ClearedStage", 0);
    }

    // デリート処理
    public void Delete()
    {
        PlayerPrefs.SetInt("ClearedStage", 0);
        PlayerPrefs.Save();
    }
}