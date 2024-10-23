using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxPrres : MonoBehaviour
{
    Rigidbody2D rb2d;

    [SerializeField] Vector2 addForceNormalize;
    [SerializeField] bool Push_Flag = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb2d.velocity.normalized);

        addForceNormalize = rb2d.velocity.normalized;

        if (Push_Flag)
        {
            addForceNormalize = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
        }
        else
        {
            addForceNormalize = new Vector2(0f,rb2d.velocity.y);
            rb2d.velocity = addForceNormalize;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pox" || collision.gameObject.tag == "Box")
        {
            Push_Flag = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {   
            Push_Flag = false;
    }
}
