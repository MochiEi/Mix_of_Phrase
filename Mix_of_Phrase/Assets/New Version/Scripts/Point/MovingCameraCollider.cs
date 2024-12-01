using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCameraCollider : MonoBehaviour
{
    [SerializeField] GameObject pox;
    private float poxPos;

    [SerializeField] GameObject cameraCollider;
    private float cameraPos;

    [SerializeField] float movingSpeed;
    private float targetPos;

    void Start()
    {

    }

    void Update()
    {
        poxPos = pox.transform.position.x;
        cameraPos = cameraCollider.transform.position.y;

        if(poxPos > this.transform.position.x)
        {
            targetPos = 5;
        }
        else
        {
            targetPos = 0;
        }

        if (cameraPos < targetPos)
        {
            cameraPos += movingSpeed * Time.deltaTime;

            if (cameraPos > targetPos)
                cameraPos = targetPos;
        }
        if (cameraPos > targetPos)
        {
            cameraPos -= movingSpeed * Time.deltaTime;

            if (cameraPos < targetPos)
                cameraPos = targetPos;
        }

        cameraCollider.transform.position = new Vector3(0, cameraPos);
    }
}
