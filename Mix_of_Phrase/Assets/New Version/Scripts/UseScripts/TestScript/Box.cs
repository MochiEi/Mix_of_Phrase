using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Box : MonoBehaviour
{
    GameObject boxObj;
    Rigidbody2D rb;
    Collider2D col;
    [SerializeField]
    Collider2D[] Tags;
    [SerializeField]
    Collider2D[] SaveObj;

    [SerializeField]
    string[] tags;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Moved(col, tags);
    }

    bool Moved(Collider2D Hit , params string[] HitTag)
    {
        int num =  Hit.OverlapCollider(new ContactFilter2D(), Tags);
        if (num > 0) 
        {
            foreach (var ObjTag in SaveObj)
            {

            }     
        }
        return true;
    }
    void Stoped(GameObject obj)
    {
        rb.velocity = new Vector2 (0, rb.velocity.y);
    }


}
