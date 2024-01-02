using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.UIElements;

public class POX_selectAnimation : MonoBehaviour
{
    public bool point = true;
    private float time = 0;
    private float speed=5;  //自機のスピード


    private Rigidbody2D rb;
    private Animator anim;
    private PolygonCollider2D poly;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        poly = GetComponent<PolygonCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (point)
        {
            time = time + Time.deltaTime;
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("run", true);
        }
        if(time > 2.77)
        {
            point = false;
            anim.SetBool("run", false);
        }
    }   
    void FixedUpdate()
    {
        if (point)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        if (time > 2.77)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
