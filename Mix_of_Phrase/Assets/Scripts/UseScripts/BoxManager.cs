using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine;
using UnityEditor.ShaderKeywordFilter;

public class BoxManager : MonoBehaviour
{
    Rigidbody2D rb2d;

    [SerializeField] Vector2 addForceNormalize;
    [SerializeField] bool Push_Flag = false;
    // [SerializeField] List<GameObject> hitObj;
    [SerializeField] string[] TagBluckList;
    [SerializeField] GameObject BoxHitList;
    [SerializeField] PressManager pressManager;


    // Start is called before the first frame update
    void Awake()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        BoxHitList = GameObject.Find("SceneManager");
        pressManager = BoxHitList.GetComponent<PressManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Push_Flag)
        {
            addForceNormalize = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
        }
        else if(!Push_Flag)
        {
            addForceNormalize = new Vector2(0f, rb2d.velocity.y);
            rb2d.velocity = addForceNormalize;
        }

        addForceNormalize = rb2d.velocity.normalized;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        for (int i = 0; i < TagBluckList.Length; i++)
        {
            if (collision.gameObject.tag != TagBluckList[i])
            {
                pressManager.HitSet(collision.gameObject, Push_Flag);
                if (pressManager.PushFlagList != null)
                {
                    Push_Flag = pressManager.PushFlagList.Last();
                }
            }
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        pressManager.OutSet(collision.gameObject, Push_Flag);
        if (pressManager.PushFlagList != null)
        {
            Push_Flag = pressManager.PushFlagList.Last();
        }
    }

}

