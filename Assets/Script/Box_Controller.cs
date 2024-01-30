using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Box_Controller : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]private bool move = false;   //é©ï™Ç™ìÆÇ¢ÇƒÇ¢ÇÈÇ©Ç«Ç§Ç©
    [SerializeField] private bool pushmove = false;   //é©ï™Ç™ìÆÇ¢ÇƒÇ¢ÇÈÇ©Ç«Ç§Ç©
    [SerializeField] private bool poxmove = false;

    [SerializeField] private bool wallstop = false;
    [SerializeField] private bool boxside = false;

    private float Raylength = 0.05f;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask boxLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boxSide(boxside = true) && poxmove == false)
        {
            move = true;
        }
        if (boxSide(boxside = true) == false || poxmove == true)
        {
            move = false;
        }

        if (poxSide())
        {
                poxmove = true;
        }
        if (poxSide() == false)
        {
            poxmove = false;
        }




        if (move == true && wallstop == false || poxmove == true && wallstop == false)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private bool poxSide()    //BoxÇÃâ°Ç…poxÇ™Ç†ÇÈÇ∆true
    {
        RaycastHit2D BoxLup = Physics2D.Raycast(new Vector2(transform.position.x - 0.401f, transform.position.y + 0.35f), Vector2.left, Raylength, playerLayer);
        RaycastHit2D BoxRup = Physics2D.Raycast(new Vector2(transform.position.x + 0.401f, transform.position.y + 0.35f), Vector2.right, Raylength, playerLayer);
        RaycastHit2D BoxLdown = Physics2D.Raycast(new Vector2(transform.position.x - 0.401f, transform.position.y - 0.35f), Vector2.left, Raylength, playerLayer);
        RaycastHit2D BoxRdown = Physics2D.Raycast(new Vector2(transform.position.x + 0.401f, transform.position.y - 0.35f), Vector2.right, Raylength, playerLayer);


        return BoxLdown.collider != null || BoxRdown.collider != null || BoxLup.collider != null || BoxRup.collider != null;
    }

    private bool boxSide(bool x)    //BoxÇÃâ°Ç…boxÇ™Ç†ÇÈÇ∆true
    {
        RaycastHit2D BoxLup = Physics2D.Raycast(new Vector2(transform.position.x - 0.401f, transform.position.y + 0.35f), Vector2.left, Raylength, boxLayer);
        RaycastHit2D BoxRup = Physics2D.Raycast(new Vector2(transform.position.x + 0.401f, transform.position.y + 0.35f), Vector2.right, Raylength, boxLayer);
        RaycastHit2D BoxLdown = Physics2D.Raycast(new Vector2(transform.position.x - 0.401f, transform.position.y - 0.35f), Vector2.left, Raylength, boxLayer);
        RaycastHit2D BoxRdown = Physics2D.Raycast(new Vector2(transform.position.x + 0.401f, transform.position.y - 0.35f), Vector2.right, Raylength, boxLayer);

        if (x == true)
        {
            if (BoxLup.collider != null)
            {
                Box_Controller otherbox = BoxLup.collider.GetComponent<Box_Controller>();
                if (otherbox.move == true || otherbox.poxmove == true)
                {
                    return true;
                }
            }
            if (BoxLdown.collider != null)
            {
                Box_Controller otherbox = BoxLdown.collider.GetComponent<Box_Controller>();
                if (otherbox.move == true || otherbox.poxmove == true)
                {
                    return true;
                }
            }
            if (BoxRup.collider != null)
            {
                Box_Controller otherbox = BoxRup.collider.GetComponent<Box_Controller>();
                if (otherbox.move == true || otherbox.poxmove == true)
                {
                    return true;
                }
            }
            if (BoxRdown.collider != null)
            {
                Box_Controller otherbox = BoxRdown.collider.GetComponent<Box_Controller>();
                if (otherbox.move == true || otherbox.poxmove == true)
                {
                    return true;
                }
            }
        }

        if (x == false)
        {
            if (BoxLup.collider != null)
            {
                Box_Controller otherbox = BoxLup.collider.GetComponent<Box_Controller>();
                if (otherbox.wallstop == true)
                {
                    return true;
                }
            }
            if (BoxLdown.collider != null)
            {
                Box_Controller otherbox = BoxLdown.collider.GetComponent<Box_Controller>();
                if (otherbox.wallstop == true)
                {
                    return true;
                }
            }
            if (BoxRup.collider != null)
            {
                Box_Controller otherbox = BoxRup.collider.GetComponent<Box_Controller>();
                if (otherbox.wallstop == true)
                {
                    return true;
                }
            }
            if (BoxRdown.collider != null)
            {
                Box_Controller otherbox = BoxRdown.collider.GetComponent<Box_Controller>();
                if (otherbox.wallstop == true)
                {
                    return true;
                }
            }
        }

        return false;
    }
    private bool boxDown()    //BoxÇÃâ∫Ç…Ç»ÇÒÇ©Ç†ÇÈÇ∆true
    {
        RaycastHit2D Box = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.401f), Vector2.down, -Raylength);
        if(Box.collider != null)
        {
            return true;
        }
        return false;
        
    }

    private void OnCollisionStay2D(Collision2D collision)   //boxÇ™ï«Ç…êGÇÍÇƒâüÇπÇ»Ç¢èÛë‘
    {
        if (collision.gameObject.CompareTag("Wall") && boxDown())
        {
            wallstop = true;
        }
        if (collision.gameObject.CompareTag("Box") && boxSide(boxside = false) && boxDown()==false)
        {
            wallstop = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(new Vector2(transform.position.x - 0.401f, transform.position.y - 0.35f), new Vector2(-Raylength, 0));
        Gizmos.DrawRay(new Vector2(transform.position.x + 0.401f, transform.position.y - 0.35f), new Vector2(Raylength, 0));
        Gizmos.DrawRay(new Vector2(transform.position.x - 0.401f, transform.position.y + 0.35f), new Vector2(-Raylength, 0));
        Gizmos.DrawRay(new Vector2(transform.position.x + 0.401f, transform.position.y + 0.35f), new Vector2(Raylength, 0));

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(new Vector2(transform.position.x - 0.401f, transform.position.y ), new Vector2(-Raylength, 0));
        Gizmos.DrawRay(new Vector2(transform.position.x + 0.401f, transform.position.y), new Vector2(Raylength, 0));

        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.401f), new Vector2(0,-Raylength));
    }
}
