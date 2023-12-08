using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POX_Controller : MonoBehaviour
{
    private float speed = 3.0f; //自機のスピード

    private Rigidbody2D rd;
    private float jumpForce = 250f; //自機のジャンプ力
    private int jumpCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            position.x -= speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            position.x += speed * Time.deltaTime;
        }

        transform.position = position;

        if (Input.GetKeyDown(KeyCode.Space)&&this.jumpCount<1)
        {
            this.rd.AddForce(transform.up * jumpForce);
            jumpCount++;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
            jumpCount = 0;
    }
}
