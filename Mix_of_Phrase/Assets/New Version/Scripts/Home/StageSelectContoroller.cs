using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectContoroller : MonoBehaviour
{
    private string sceneName;

    private GameDateController gameDate;
    private int clearStage;

    private List<GameObject> buttonList = new List<GameObject>();

    void Start()
    {
        sceneName = "Home";

        gameDate = GameObject.Find("GameManager").GetComponent<GameDateController>();
        clearStage = gameDate.Load() + 1;

        foreach (Transform child in this.transform)
        {
            buttonList.Add(child.gameObject);
        }

        for (int i = 0; i < buttonList.Count; i++)
        {
            Text number = buttonList[i].transform.GetChild(0).gameObject.GetComponent<Text>();
            Image frame = buttonList[i].GetComponent<Image>();
            Button Activ = buttonList[i].GetComponent<Button>();

            if(i <= clearStage)
            {
                number.color = new Color32(50, 50, 50, 255);
                frame.color = new Color32(0, 0, 0, 255);
                Activ.enabled = true;
            }
            else
            {
                number.color = new Color32(50, 50, 50, 110);
                frame.color = new Color32(0, 0, 0, 110);
                Activ.enabled = false;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            gameDate.Save(gameDate.Load() + 1);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            PlayerPrefs.DeleteKey("ClearedStage");
        }
    }

    public void ChangeScene00()
    {
        sceneName = "Stage 00";
    }
}
