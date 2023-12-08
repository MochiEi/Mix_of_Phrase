using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POX_Controller : MonoBehaviour
{
    private float speed = 1.0f; //自機のスピード
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            position.x -= speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            position.x += speed * Time.deltaTime;
        }

        transform.position = position;
    }
}
