using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToSelectStage : MonoBehaviour
{
    public void ToHome()
    {
        SceneManager.LoadScene("Home");
    }
    public void ToStage00()
    {
        SceneManager.LoadScene("Stage 00");
    }
    public void ToStage01()
    {
        SceneManager.LoadScene("Stage 01");
    }
    public void ToStage02()
    {
        SceneManager.LoadScene("Stage 02");
    }
    public void ToStage03()
    {
        SceneManager.LoadScene("Stage 03");
    }
}
