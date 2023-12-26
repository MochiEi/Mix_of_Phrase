using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class POX_Controller : MonoBehaviour
{
    private float speed=3;  //自機のスピード
    private float jumpForce = 250f; //自機のジャンプ力

    private Rigidbody2D rb;
    private int jumpCount = 0;  //ジャンプをカウントする変数

    public GameObject prefab;   //ここに生成したいアイテムとか入れよう(public変数1つにつき1つまで)


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !(rb.velocity.y < -0.5f) && !(rb.velocity.y > 1f) && this.jumpCount < 1)
        {
            this.rb.AddForce(transform.up * jumpForce);
            jumpCount++;
        }

        if (Input.GetKeyDown(KeyCode.Return)) // スペースキーを押したとき
        {
            Vector3 spawnPosition = transform.position + Vector3.up; // 真上の位置を計算
            Instantiate(prefab, spawnPosition, Quaternion.identity); // プレハブを生成
        }

    }   
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            jumpCount = 0;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box_Top"))
        {
            jumpCount = 0;
        }
    }
}
