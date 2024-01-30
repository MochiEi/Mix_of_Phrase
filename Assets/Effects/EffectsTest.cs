using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsTest : MonoBehaviour
{

    // プレハブ格納用
    public GameObject BOXadvent;
    public GameObject BOXbreak;
    public GameObject FrameEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // 生成位置
            Vector3 pos = this.transform.localPosition;
            // プレハブを指定位置に生成
            Instantiate(BOXadvent, pos, Quaternion.Euler(-90f, 0f, 0f));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            // 生成位置
            Vector3 pos = this.transform.localPosition;
            // プレハブを指定位置に生成
            Instantiate(BOXbreak, pos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            // 生成位置
            Vector3 pos = this.transform.localPosition;
            // プレハブを指定位置に生成
            Instantiate(FrameEffect, pos, Quaternion.identity);
        }
    }
}
