using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    Color Color = new Color(1.0f, 0f, 0.0f, 1.0f);
    public Image button;

    // Start is called before the first frame update
    void Start()
    {
        button=GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("aaaaa");
            this.GetComponent<Image>().color = Color;
        }
        else
        {
            this.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0);
        }
    }

    

}
