using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class TestPress : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField]
    bool push;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HitSet(GameObject pushflag)
    {
        Debug.Log("HitSet");
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (!push)
            {
                if (gameObjects[i].tag == "Pox")
                {
                    push = true;
                }
                else if (gameObjects[i].tag == "Box")
                {
                    gameObjects.RemoveAt(i);
                }
            }
            else if (gameObjects[i].tag == "Box" || gameObjects[i].tag == "Pox")
            {
                pushflag.gameObject.tag = "BoxPress";
                gameObjects.RemoveAll(all => all.tag == "Pox");
            }
        }
        //gameObjects.Clear();
    }

    public void OutSet(GameObject Stopflag , bool stop)
    {
        foreach (GameObject go in gameObjects)
        {
            if (go.tag != "Pox")
            {
                stop = false;
                Stopflag.gameObject.tag = "Box";
                Debug.Log(stop);
            }
            else
            {
                stop = true;
                Debug.Log(stop);
            }
        }
    }
}

