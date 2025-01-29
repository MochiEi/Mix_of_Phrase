using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class IsSelect
{
    public static bool isSelected = false;
    public static bool isActive = false;
}

public class ToSelectStage : MonoBehaviour
{
    public void ToHome()
    {
        SceneManager.LoadScene("Home");
        IsSelect.isSelected = true;
    }
    public void ToStage00()
    {
        SceneManager.LoadScene("Stage 00");
        IsSelect.isSelected = true;
    }
    public void ToStage01()
    {
        SceneManager.LoadScene("Stage 01");
        IsSelect.isSelected = true;
    }
    public void ToStage02()
    {
        SceneManager.LoadScene("Stage 02");
        IsSelect.isSelected = true;
    }
    public void ToStage03()
    {
        SceneManager.LoadScene("Stage 03");
        IsSelect.isSelected = true;
    }
}
