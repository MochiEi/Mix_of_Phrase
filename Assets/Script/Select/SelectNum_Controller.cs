using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectNum_Controller : MonoBehaviour
{
    private Rigidbody2D rb;

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
        if (!boxSideR())
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

        if (Rmove == true || Lmove == true || poxmove == true)
        {
            if (wallstop == false)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                //rb.mass = 0.1f;
            }
        }
        if (Rmove == false && Lmove == false && poxmove == false || wallstop == true)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
           // rb.mass = 1;
        }
    }

    private bool poxSide()    //Box‚Ì‰¡‚Épox‚ª‚ ‚é‚Ætrue
    {
        RaycastHit2D BoxLup = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y + 0.35f), Vector2.left, Raylength, playerLayer);
        RaycastHit2D BoxRup = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y + 0.35f), Vector2.right, Raylength, playerLayer);
        RaycastHit2D BoxLdown = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.35f), Vector2.left, Raylength, playerLayer);
        RaycastHit2D BoxRdown = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y - 0.35f), Vector2.right, Raylength, playerLayer);


        return BoxLdown.collider != null || BoxRdown.collider != null || BoxLup.collider != null || BoxRup.collider != null;
    }

    private bool boxSideL()    //Box‚Ì‰¡‚Ébox‚ª‚ ‚é‚Ætrue
    {
        RaycastHit2D BoxLup = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y + 0.35f), Vector2.left, Raylength, boxLayer);
        RaycastHit2D BoxLdown = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.35f), Vector2.left, Raylength, boxLayer);

        if (BoxLup.collider != null)
        {
            SelectNum_Controller otherbox = BoxLup.collider.GetComponent<SelectNum_Controller>();
            if (otherbox.Lmove == true || otherbox.poxmove == true)
            {
                return true;
            }
        }
        if (BoxLdown.collider != null)
        {
            SelectNum_Controller otherbox = BoxLdown.collider.GetComponent<SelectNum_Controller>();
            if (otherbox.Lmove == true || otherbox.poxmove == true)
            {
                return true;
            }
        }
        return false;
    }

    private bool boxSideR()    //Box‚Ì‰¡‚Ébox‚ª‚ ‚é‚Ætrue
    {
        RaycastHit2D BoxRup = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y + 0.35f), Vector2.right, Raylength, boxLayer);
        RaycastHit2D BoxRdown = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y - 0.35f), Vector2.right, Raylength, boxLayer);

        if (BoxRup.collider != null)
        {
            SelectNum_Controller otherbox = BoxRup.collider.GetComponent<SelectNum_Controller>();
            if (otherbox.Rmove == true || otherbox.poxmove == true)
            {
                Rmove = true;
                return true;
            }
        }
        if (BoxRdown.collider != null)
        {
            SelectNum_Controller otherbox = BoxRdown.collider.GetComponent<SelectNum_Controller>();
            if (otherbox.Rmove == true || otherbox.poxmove == true)
            {
                Rmove = true;
                return true;
            }
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.35f), new Vector2(-Raylength, 0));
        Gizmos.DrawRay(new Vector2(transform.position.x + 0.5f, transform.position.y - 0.35f), new Vector2(Raylength, 0));
        Gizmos.DrawRay(new Vector2(transform.position.x - 0.5f, transform.position.y + 0.35f), new Vector2(-Raylength, 0));
        Gizmos.DrawRay(new Vector2(transform.position.x + 0.5f, transform.position.y + 0.35f), new Vector2(Raylength, 0));

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(new Vector2(transform.position.x - 0.5f, transform.position.y), new Vector2(-Raylength, 0));
        Gizmos.DrawRay(new Vector2(transform.position.x + 0.5f, transform.position.y), new Vector2(Raylength, 0));
    }
}
