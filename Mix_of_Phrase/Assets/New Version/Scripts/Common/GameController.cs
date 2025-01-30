using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class IsSelect
{
    public static bool isSelected = false;
    public static bool isActive = false;
}

public class GameController : MonoBehaviour
{   
    public static GameController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // �d��������폜
        }
    }

    //------------------- �Z�[�u���� -------------------//
    public void SaveClearStage(int stageNumber)
    {
        PlayerPrefs.SetInt("ClearedStage", stageNumber);
        PlayerPrefs.Save();
    }

    public int LoadSaveData()
    {
        return PlayerPrefs.GetInt("ClearedStage", 1);
    }

    //------------------- �V�[���ړ� -------------------//
    public void ToHome()
    {
        SceneManager.LoadScene("Home");
        IsSelect.isSelected = true;
    }

    public void LoadStage(string stageName)
    {
        SceneManager.LoadScene(stageName);
        IsSelect.isSelected = true;
    }
}