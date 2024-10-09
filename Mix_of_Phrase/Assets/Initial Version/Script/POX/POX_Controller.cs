using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class POX_Controller : MonoBehaviour
{
    private float speed=3;  //自機のスピード
    private float jumpForce = 250f; //自機のジャンプ力

    private int jumpCount = 0;
    private bool down;

    private Animator anime;
    private Rigidbody2D rb;
    private PolygonCollider2D Pcollider;      

    //////////////////////////////////////////////////////////////
    [SerializeField] private string[] tagName;
    //////////////////////////////////////////////////////////////

    //[SerializeField] private LayerMask groundLayer;
    //[SerializeField] private LayerMask boxLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        Pcollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isJump())
        {
            this.rb.AddForce(transform.up * jumpForce);
        }

        //タグで処理するように改変
        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded() || Input.GetKeyDown(KeyCode.Space) && isBoxed())
        //{
        //    this.rb.AddForce(transform.up * jumpForce);
        //}

        Animation();
        isDown();
    }

    private void Animation()
    {
        //歩行アニメーション
        if (Input.GetKey(KeyCode.A) && !down)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anime.SetBool("walk", true);            
        }
        else if (Input.GetKey(KeyCode.D) && !down)
        {
            transform.localScale = new Vector3(1, 1, 1);
            anime.SetBool("walk", true);
        }
        else
        {
            anime.SetBool("walk", false);
        }

        //しゃがむアニメーション
        if (down)
        {
            anime.SetBool("down", true);
        }
        else
        {
            anime.SetBool("down", false);
        }

        //ジャンプアニメーション
        int layerMask = 1 << 7 | 1 << 8;
        layerMask = ~layerMask;
        RaycastHit2D jumpL = Physics2D.Raycast(new Vector2(transform.position.x - 0.37f, transform.position.y), Vector2.down, 0.63f, layerMask);
        RaycastHit2D jumpR = Physics2D.Raycast(new Vector2(transform.position.x + 0.37f, transform.position.y), Vector2.down, 0.63f, layerMask);
        if (jumpCount == 0 && Input.GetKey(KeyCode.Space))
        {
            jumpCount = 1;
            anime.SetBool("jump", true);
        }
        else if (jumpCount == 1 && !isJump())
        {
            jumpCount = 2;
        }
        foreach (string t in tagName)
        {
            if (jumpL.collider != null && jumpL.collider.gameObject.tag == t)
            {
                jumpCount = 0;
                anime.SetBool("jump", false);
            }
            if (jumpR.collider != null && jumpR.collider.gameObject.tag == t)
            {
                jumpCount = 0;
                anime.SetBool("jump", false);
            }
        }
    }

    private void isDown()
    {
        Vector2[] points = Pcollider.points;

        if (Input.GetKey(KeyCode.S))
        {
            points[0] = new Vector2(0.4f, 0.32f);
            points[1] = new Vector2(-0.4f, 0.32f);
            Pcollider.SetPath(0, points);
            down = true;
        }
        else
        {
            points[0] = new Vector2(0.4f, 0.52f);
            points[1] = new Vector2(-0.4f, 0.52f);
            Pcollider.SetPath(0, points);
            down = false;
        }
    }

    private bool isJump()
    {
        bool canJump = false;
        int layerMask = 1 << 7 | 1 << 8;
        layerMask = ~layerMask;

        RaycastHit2D jumpL = Physics2D.Raycast(new Vector2(transform.position.x - 0.37f, transform.position.y), Vector2.down, 0.63f, layerMask);
        RaycastHit2D jumpR = Physics2D.Raycast(new Vector2(transform.position.x + 0.37f, transform.position.y), Vector2.down, 0.63f, layerMask);

        foreach (string t in tagName)
        {
            if (jumpL.collider != null && jumpL.collider.gameObject.tag == t)
            {
                canJump = true;
            }
            if (jumpR.collider != null && jumpR.collider.gameObject.tag == t)
            {
                canJump = true;
            }
        }

        if(canJump) return true;

        return false;
    }

    //private bool isGrounded()   //着地判定
    //{
    //    RaycastHit2D jumpL = Physics2D.Raycast(new Vector2(transform.position.x - 0.39f, transform.position.y), Vector2.down, 0.63f, groundLayer);
    //    RaycastHit2D jumpR = Physics2D.Raycast(new Vector2(transform.position.x + 0.39f, transform.position.y), Vector2.down, 0.6f, groundLayer);
    //    return jumpL.collider != null || jumpR.collider != null;
    //}
    //private bool isBoxed()   //着地判定
    //{
    //    RaycastHit2D jumpL = Physics2D.Raycast(new Vector2(transform.position.x - 0.39f, transform.position.y), Vector2.down, 0.63f, boxLayer);
    //    RaycastHit2D jumpR = Physics2D.Raycast(new Vector2(transform.position.x + 0.39f, transform.position.y), Vector2.down, 0.63f, boxLayer);
    //    return jumpL.collider != null || jumpR.collider != null;
    //}

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) && !down)
        {           
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D) && !down)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector2(transform.position.x - 0.37f, transform.position.y), new Vector2(0, -0.63f));
        Gizmos.DrawRay(new Vector2(transform.position.x + 0.37f, transform.position.y), new Vector2(0, -0.63f));
    }

}
