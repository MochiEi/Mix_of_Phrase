using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BoxMove : MonoBehaviour
{
    GameObject Box;
    Collider2D boxColl;
    [SerializeField]
    Collider2D[] HitObj = new Collider2D[1];
    Rigidbody2D rb;
    [SerializeField]
    string[] MoveTags = new string[2];

    [SerializeField]
    Vector2[] DirectVec;
    [SerializeField]
    Vector2 Direction;

    //bool MovedNow;
    bool StopNow;
    // Start is called before the first frame update
    void Start()
    {
        Box = this.gameObject;
        boxColl = Box.GetComponent<Collider2D>();
        rb = Box.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MovedNow(boxColl, MoveTags))
        {
            int countHit = HitObj.Length;
        }
        else
        {
            Debug.Log("AA");
            Stoped(this.gameObject);
        }
    }

    bool MovedNow(Collider2D Coll, params string[] ListTags)
    {
        int Count = Coll.OverlapCollider(new ContactFilter2D(), HitObj);

        if (Count > 0)
        {
            Direction = (HitObj[0].ClosestPoint(transform.position) - (Vector2)transform.position).normalized;
            if (Direction.x == -1 || Direction.x == 1)
            {
                foreach (var HitColl in HitObj)
                {
                    if (TargetTagResarch(HitColl, ListTags))
                    {
                        if (HitColl.tag == "Pox")
                        {
                            this.gameObject.tag = "PoxPress";
                            return true;
                        }
                        else if (HitObj[0].tag == "PoxPress")
                        {
                            this.gameObject.tag = "PoxPress";
                            return true;
                        }
                        else
                        {
                            return false;
                        }
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

    void Stoped(GameObject setObj)
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public static bool TargetTagResarch(Collider2D collder2d, params string[] tags)
    {
        return tags.Any(tag => collder2d.CompareTag(tag));
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CountHit(HitObj);
    }

    void CountHit(Collider2D[] countColl)
    {
        if (HitObj[0].tag == countColl[0].tag)
        {
            Debug.Log(HitObj[0].tag +"=="+ countColl[0].tag);
            Stoped(this.gameObject);
        }
    }
}
   
