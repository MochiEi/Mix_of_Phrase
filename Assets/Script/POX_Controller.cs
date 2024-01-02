using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.UIElements;

public class POX_Controller : MonoBehaviour
{
    private float speed=3;  //自機のスピード
    private float jumpForce = 250f; //自機のジャンプ力

    private Rigidbody2D rb;
    private int jumpCount = 0;  //ジャンプをカウントする変数

    private Animator anim;
    private PolygonCollider2D poly;

    public GameObject prefab;   //ここに生成したいアイテムとか入れよう(public変数1つにつき1つまで)
    public GameObject button;
    Vector2[] points;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        poly = GetComponent<PolygonCollider2D>();

        rb = GetComponent<Rigidbody2D>();
        points = poly.points;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space) && !(rb.velocity.y < -0.5f) && !(rb.velocity.y > 1f) && this.jumpCount < 1)
        {
            this.rb.AddForce(transform.up * jumpForce);
            jumpCount++;
        }

        if (Input.GetKeyDown(KeyCode.Return)) // スペースキーを押したとき
        {
            Vector3 spawnPosition = transform.position + Vector3.up; // 真上の位置を計算
            Instantiate(prefab, spawnPosition, Quaternion.identity); // プレハブを生成
        }

        if (jumpCount == 1)
        {
            anim.SetBool("jump", true);
            points[0] = new Vector2(-0.4f, 0.6f);
            points[5] = new Vector2(0.4f, 0.6f); ;
            poly.points = points;
        }
        else if (jumpCount != 1)
        {
            anim.SetBool("jump", false);
        }
        if(Input.GetKey(KeyCode.S) && jumpCount != 1)
        {
            anim.SetBool("down", true);
            points[0] = new Vector2(-0.4f, 0.3f);
            points[5] = new Vector2(0.4f, 0.3f);
            poly.points = points;
        }
        else if(!Input.GetKey(KeyCode.S) && jumpCount != 1)
        {
            anim.SetBool("down", false);
            points[0] = new Vector2(-0.4f, 0.5f);
            points[5] = new Vector2(0.4f, 0.5f);
            poly.points = points;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("run", true); 

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
    }   
    void FixedUpdate()
    {
        if (!Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else if (!Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
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
