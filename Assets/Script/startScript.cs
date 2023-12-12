using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startScript : MonoBehaviour
{
    public float speed = 1.0f;
    public bool start = false;

    private Text text;
    private float time;

    void Start()
    {
        text = this.gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            start = true;
        }

        if (start)
        {
            text.color = GetAlphaColor(text.color);
        }
    }

    //Alpha’l‚ğXV‚µ‚ÄColor‚ğ•Ô‚·
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime *    speed;
        color.a = 1.0f - time;

        return color;
    }
}
