using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NumButtonController : MonoBehaviour
{
    [SerializeField] Transform[] buttons;
    private Color activeColor = new Color(0, 0, 0, 1.0f);
    private Color inactiveColor = new Color(0.7f, 0.7f, 0.7f, 1.0f);

    void Start()
    {
        buttons = transform.Cast<Transform>().ToArray();

        ActiveChack();
    }

    public void ActiveChack()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i > GameController.Instance.LoadSaveData())
            {
                buttons[i].GetComponent<Button>().enabled = false;
                buttons[i].GetChild(0).GetComponent<Text>().color = inactiveColor;
            }
            else
            {
                buttons[i].GetComponent<Button>().enabled = true;
                buttons[i].GetChild(0).GetComponent<Text>().color = activeColor;
            }
        }
    }

    public void ToStage(int stageIndex)
    {
        string sceneName = $"Stage 0{stageIndex}";
        GameController.Instance.LoadStage(sceneName);
    }

    public void Delete()
    {
        PlayerPrefs.SetInt("ClearedStage", 1);
        PlayerPrefs.Save();
    }
}