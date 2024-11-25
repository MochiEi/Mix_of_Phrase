using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine;
using UnityEditor.ShaderKeywordFilter;

public class BoxPrres : MonoBehaviour
{
    Rigidbody2D rb2d;

    [SerializeField] Vector2 addForceNormalize;
    [SerializeField] bool Push_Flag = false;
    // [SerializeField] List<GameObject> hitObj;
    [SerializeField] string[] TagBluckList;
    [SerializeField] GameObject test;
    [SerializeField] TestPress testPress;
    [SerializeField] int  Pox =0;


    // Start is called before the first frame update
    void Awake()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        test = GameObject.Find("BoxHitList");
        testPress = test.GetComponent<TestPress>();
    }

    // Update is called once per frame
    void Update()
    {
    
        if (this.gameObject.tag == "BoxPress")
        {
            Push_Flag = true;
        }
        else
        {
            Push_Flag = false;
        }
        addForceNormalize = rb2d.velocity.normalized;
        if (Push_Flag)
        {
            addForceNormalize = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
        }
        else
        {
            addForceNormalize = new Vector2(0f, rb2d.velocity.y);
            rb2d.velocity = addForceNormalize;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Hit");
        if (this.gameObject.tag != "BoxPress")
        {
            Debug.Log("Load");
            for (int i = 0; i < TagBluckList.Length; i++)
            {
                if (collision.gameObject.tag != TagBluckList[i])
                {
                    Debug.Log("Stock");
                    testPress.gameObjects.Add(collision.gameObject);
                    testPress.HitSet(this.gameObject);
                }
            }
        }
    }

}
    
