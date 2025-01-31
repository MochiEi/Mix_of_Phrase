using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Box : MonoBehaviour
{
    GameObject boxObj;
    Rigidbody2D rb;
    Collider2D col;
    [SerializeField]
    Collider2D[] HitObj;

    [SerializeField]
    string[] tags;

    [SerializeField]
    Vector2 direction;
    Vector2[] directionVec = new Vector2[1];

    // Start is called before the first frame update
    void Start()
    {
        boxObj = this.gameObject;
        rb = boxObj.GetComponent<Rigidbody2D>();
        col = boxObj.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Moved(col, tags))
        {
            Debug.Log("Press");
        }
        else
        {
            foreach (var hit in HitObj)
            {
                if (hit == null) break;
                if (HitObj[0].tag != HitObj[1].tag)
                {

                }
                else if (HitObj[0].tag == HitObj[1].tag)
                {
                    if (HitObj[0].tag == "Box")
                    {
                        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                    }
                }
            }
        }
    }

    bool Moved(Collider2D Hit , params string[] HitTag)
    {
        int num =  Hit.OverlapCollider(new ContactFilter2D(), HitObj);
        if (num > 0)
        {
            direction = (HitObj[0].ClosestPoint(transform.position) - (Vector2)transform.position).normalized;

            if (direction.x >=0f  || direction.x >= -0f)
            {
                foreach (var ObjTag in HitObj)
                {
                    if (TargetTagResarch(ObjTag, tags))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        return false;
    }
    void Stoped(GameObject obj)
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public static bool TargetTagResarch(Collider2D collder2d, params string[] tags)
    {
        return tags.Any(tag => collder2d.CompareTag(tag));
    }
}
