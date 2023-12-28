using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveText : MonoBehaviour
{
    public GameObject parentObject;
    Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        position.x = parentObject.transform.position.x * 108 + 960;
        position.y = parentObject.transform.position.y * 108 + 540;

        this.transform.position = position;
    }
}
