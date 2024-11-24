using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestPress : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> gameObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObjects.RemoveAll(obj => obj == gameObject);
    }
}
