using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class POX_Controller : MonoBehaviour
{
    private float speed=3;  //自機のスピード
    private float jumpForce = 250f; //自機のジャンプ力

    private Rigidbody2D rb;

    [SerializeField]private LayerMask groundLayer;
    [SerializeField] private LayerMask boxLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded()|| Input.GetKeyDown(KeyCode.Space) && isBoxed())
        {
            this.rb.AddForce(transform.up * jumpForce);
        }
    }

    private bool isGrounded()   //着地判定
    {
        RaycastHit2D jumpL = Physics2D.Raycast(new Vector2(transform.position.x - 0.39f, transform.position.y), Vector2.down, 0.6f, groundLayer);
        RaycastHit2D jumpR = Physics2D.Raycast(new Vector2(transform.position.x + 0.39f, transform.position.y), Vector2.down, 0.6f, groundLayer);
        return jumpL.collider != null || jumpR.collider != null;
    }
    private bool isBoxed()   //着地判定
    {
        RaycastHit2D jumpL = Physics2D.Raycast(new Vector2(transform.position.x - 0.39f, transform.position.y), Vector2.down, 0.6f, boxLayer);
        RaycastHit2D jumpR = Physics2D.Raycast(new Vector2(transform.position.x + 0.39f, transform.position.y), Vector2.down, 0.6f, boxLayer);
        return jumpL.collider != null || jumpR.collider != null;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector2(transform.position.x - 0.39f, transform.position.y), new Vector2(0, -0.6f));
        Gizmos.DrawRay(new Vector2(transform.position.x + 0.39f, transform.position.y), new Vector2(0, -0.6f));
    }

}
