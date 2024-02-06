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
    [SerializeField] private bool poxmove = false;
    [SerializeField] private bool Lmove = false;
    [SerializeField] private bool Rmove = false;

    [SerializeField] private bool wallstop = false;

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
        if (boxSideR() && poxmove == false)
        {
            Rmove = true;
        }
        if(!boxSideR())
        {
            Rmove = false;
        }
        if (boxSideL() && poxmove == false)
        {
            Lmove = true;
        }
        if (!boxSideL())
        {
            Lmove = false;
        }

        if (poxSide())
        {
                poxmove = true;
        }
        if (poxSide() == false)
        {
            poxmove = false;
        }

        if(wallStop()&&boxDown())
        {
            wallstop= true;
        }


        if (Rmove==true||Lmove==true||poxmove==true)
        {
            if(wallstop==false)
            {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        if(Rmove == false && Lmove == false && poxmove == false || wallstop==true)
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

    private bool boxSideL()    //BoxÇÃâ°Ç…boxÇ™Ç†ÇÈÇ∆true
    {
        RaycastHit2D BoxLup = Physics2D.Raycast(new Vector2(transform.position.x - 0.401f, transform.position.y + 0.35f), Vector2.left, Raylength, boxLayer);
        RaycastHit2D BoxLdown = Physics2D.Raycast(new Vector2(transform.position.x - 0.401f, transform.position.y - 0.35f), Vector2.left, Raylength, boxLayer);

            if (BoxLup.collider != null)
            {
                Box_Controller otherbox = BoxLup.collider.GetComponent<Box_Controller>();
                if (otherbox.Lmove == true || otherbox.poxmove == true)
                {
                    return true;
                }
            }
            if (BoxLdown.collider != null)
            {
                Box_Controller otherbox = BoxLdown.collider.GetComponent<Box_Controller>();
            if (otherbox.Lmove == true || otherbox.poxmove == true)
            {
                return true;
            }
        }
        return false;
    }

    private bool boxSideR()    //BoxÇÃâ°Ç…boxÇ™Ç†ÇÈÇ∆true
    {
        RaycastHit2D BoxRup = Physics2D.Raycast(new Vector2(transform.position.x + 0.401f, transform.position.y + 0.35f), Vector2.right, Raylength, boxLayer);
        RaycastHit2D BoxRdown = Physics2D.Raycast(new Vector2(transform.position.x + 0.401f, transform.position.y - 0.35f), Vector2.right, Raylength, boxLayer);

        if (BoxRup.collider != null)
        {
            Box_Controller otherbox = BoxRup.collider.GetComponent<Box_Controller>();
            if (otherbox.Rmove == true || otherbox.poxmove == true)
            {
                Rmove = true;
                return true;
            }
        }
        if (BoxRdown.collider != null)
        {
            Box_Controller otherbox = BoxRdown.collider.GetComponent<Box_Controller>();
            if (otherbox.Rmove == true || otherbox.poxmove == true)
            {
                Rmove = true;
                return true;
            }
        }
        return false;
    }

    private bool boxDown()    //BoxÇÃâ∫Ç…Ç»ÇÒÇ©Ç†ÇÈÇ∆true
    {
        RaycastHit2D BoxL = Physics2D.Raycast(new Vector2(transform.position.x - 0.35f, transform.position.y - 0.401f), Vector2.down, -Raylength);
        RaycastHit2D BoxR = Physics2D.Raycast(new Vector2(transform.position.x + 0.35f, transform.position.y - 0.401f), Vector2.down, -Raylength);
        if (BoxL.collider != null|| BoxR.collider != null)
        {
            return true;
        }
        return false;
        
    }

    private bool wallStop() //ï«Ç…êGÇÍÇƒâüÇπÇ»Ç≠Ç»ÇÈÇ©Ç«Ç§Ç©        
    {
        RaycastHit2D BoxLup = Physics2D.Raycast(new Vector2(transform.position.x - 0.401f, transform.position.y + 0.35f), Vector2.left, Raylength);
        RaycastHit2D BoxRup = Physics2D.Raycast(new Vector2(transform.position.x + 0.401f, transform.position.y + 0.35f), Vector2.right, Raylength);
        RaycastHit2D BoxLdown = Physics2D.Raycast(new Vector2(transform.position.x - 0.401f, transform.position.y - 0.35f), Vector2.left, Raylength);
        RaycastHit2D BoxRdown = Physics2D.Raycast(new Vector2(transform.position.x + 0.401f, transform.position.y - 0.35f), Vector2.right, Raylength);

        if (BoxLup.collider != null&&BoxLdown.collider!=null) 
        {
            if (BoxLup.collider.CompareTag("Wall")&& BoxLdown.collider.CompareTag("Wall"))
            {
                return true;
            }
            if (BoxLup.collider.CompareTag("Box") && BoxLdown.collider.CompareTag("Box"))
            {
                Box_Controller otherboxup = BoxLup.collider.GetComponent<Box_Controller>();
                Box_Controller otherboxdown = BoxLdown.collider.GetComponent<Box_Controller>();
                if (otherboxup.wallstop == true || otherboxdown.wallstop == true)
                {
                    return true;
                }
            }
        }

        if (BoxRup.collider != null && BoxRdown.collider != null)
        {
            if (BoxRup.collider.CompareTag("Wall") && BoxRdown.collider.CompareTag("Wall"))
            {
                return true;
            }
            if (BoxRup.collider.CompareTag("Box") && BoxRdown.collider.CompareTag("Box"))
            {
                Box_Controller otherboxup = BoxRup.collider.GetComponent<Box_Controller>();
                Box_Controller otherboxdown = BoxRdown.collider.GetComponent<Box_Controller>();
                if (otherboxup.wallstop == true || otherboxdown.wallstop == true)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && wallStop())//boxÇâüÇπÇ»Ç¢èÛë‘
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
        Gizmos.DrawRay(new Vector2(transform.position.x - 0.35f, transform.position.y - 0.401f), new Vector2(0,-Raylength));
        Gizmos.DrawRay(new Vector2(transform.position.x + 0.35f, transform.position.y - 0.401f), new Vector2(0, -Raylength));
    }
}
