using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    Rigidbody2D rb2d;
    Collider2D col;
    [SerializeField]
    string[] TagList;
    [SerializeField] Vector2 addForceNormalize;
    [SerializeField] bool Push_Flag = false;
    [SerializeField] GameObject BoxHitList;
    [SerializeField] PressManager pressManager;

    // Start is called before the first frame update
    void Awake()
    {
        col = this.GetComponent<Collider2D>();
        rb2d = this.GetComponent<Rigidbody2D>();
        BoxHitList = GameObject.Find("SceneManager");
        pressManager = BoxHitList.GetComponent<PressManager>();
    }

    private void Update()
    {
        pressManager.Flagment(col);
        if (pressManager.PushFlagList != null)
        {
            //Push_Flag = pressManager.PushFlagList.Last();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Push_Flag)
        {
            Debug.Log("BBB");
            addForceNormalize = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
        }
        else if (!Push_Flag)
        {
            Debug.Log("AAA");
            addForceNormalize = new Vector2(0f, rb2d.velocity.y);
            rb2d.velocity = addForceNormalize;
        }

        addForceNormalize = rb2d.velocity.normalized;
    }
}

