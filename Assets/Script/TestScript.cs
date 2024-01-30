using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject TestPox;
    Vector3 pos = new Vector3( 0, 0, 0 );

    public int time = 0; 

    // Start is called before the first frame update
    void Start()
    {
        pos = TestPox.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (pos.x < -3)
        {
            pos.x += time * Time.deltaTime;
        }

        TestPox.transform.position = pos;
    }
}
