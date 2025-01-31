using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ForceVector;
public class FlagManager : MonoBehaviour
{

    [SerializeField]
    string[] TagList;
    [SerializeField]
    List<bool> pushFlagList;
    [SerializeField]
    List<GameObject> gameObjects;
    [SerializeField]
    List<Collider2D>HitObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pushFlagList != null && gameObjects != null)
        {
            if (pushFlagList.Count > 10)
            {
                pushFlagList.RemoveAt(1);
            }
            if (gameObjects.Count > 10)
            {
                gameObjects.RemoveAt(1);
            }
        }
    }

    void HitCount(Collider2D collider)
    {
        int count = collider.OverlapCollider(new ContactFilter2D(), HitObj);
        if (count > 0)
        { 
            pushFlagList.Add(true);
        }
        else
        {
            //false
        }
    }
}
