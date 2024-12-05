using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBlockController : MonoBehaviour
{
    private ActiveColorBlock activeColor;
    private enum BlockColor
    {
        Red, Blue
    };
    [SerializeField] BlockColor color;
    private string blockColor;

    private GameObject[] block = new GameObject[2];
    private bool isEnable;

    void Start()
    {
        activeColor = GameObject.Find("SceneManager").GetComponent<ActiveColorBlock>();
        blockColor = color.ToString();

        foreach (Transform child in transform)
        {
            if (child.name == "OffLine")
                block[0] = child.gameObject;
            if (child.name == "OnLine")
                block[1] = child.gameObject;
        }
    }

    void Update()
    {
        block[0].SetActive(blockColor != activeColor.ActiveColor());
        block[1].SetActive(!block[0].activeSelf);
    }
}
