using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool move = false;
    //private bool 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, Mathf.Floor(transform.position.y * 10000) / 10000, transform.position.z);

        if (move ==true)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            //rb.mass = 1;
        }
        if(move ==false)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            //rb.mass = 5;
        }

    }


    void OnCollisionExit2D(Collision2D collision)
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("POX_Side"))
        {
            move = false;
        }
        if (collision.gameObject.CompareTag("Box_Right")|| collision.gameObject.CompareTag("Box_Left"))
        {
            move = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("POX_Side"))
        {
            move = true;
        }
        if (collision.gameObject.CompareTag("Box_Right"))
        {
            Transform parentTransform = collision.transform.parent;
            Rigidbody2D rbR = parentTransform.GetComponent<Rigidbody2D>();
            Vector2 speedR = rbR.velocity;
            if (speedR.x > 0 || speedR.x < 0)
            {
                move = true;
            }
            else if (move == true && rb.velocity.x == 0)
            {
                move = false;
                Debug.Log("no");
            }
        }
        if (collision.gameObject.CompareTag("Box_Left"))
        {
            Transform parentTransform = collision.transform.parent;
            Rigidbody2D rbL = parentTransform.GetComponent<Rigidbody2D>();
            Vector2 speedL = rbL.velocity;
            if (speedL.x > 0.0 || speedL.x < 0)
            {
                move = true;
            }
            else if (move == true && rb.velocity.x == 0)
            {
                move = false;
            }
        }
    }
}
