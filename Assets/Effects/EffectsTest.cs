using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectsTest : MonoBehaviour
{

    // プレハブ格納用
    public GameObject BOXadvent;
    public GameObject BOXbreak;
    public GameObject FrameEffect;

    public float speed=3;
    private Rigidbody2D rb;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 生成位置
            Vector3 pos = this.transform.localPosition;
            // プレハブを指定位置に生成
            Instantiate(BOXadvent, pos, Quaternion.Euler(-90f, 0f, 0f));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            // 生成位置
            Vector3 pos = this.transform.localPosition;
            // プレハブを指定位置に生成
            Instantiate(BOXbreak, pos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 生成位置
            Vector3 pos = this.transform.localPosition;
            // プレハブを指定位置に生成
            Instantiate(FrameEffect, pos, Quaternion.identity);
        }


        float horizontalKey = Input.GetAxis("Horizontal");

        //右入力で左向きに動く
        if (horizontalKey > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        //左入力で左向きに動く
        else if (horizontalKey < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        //ボタンを話すと止まる
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    // 物理演算をしたい場合はFixedUpdateを使うのが一般的
    void FixedUpdate()
    {
        
    }
}


