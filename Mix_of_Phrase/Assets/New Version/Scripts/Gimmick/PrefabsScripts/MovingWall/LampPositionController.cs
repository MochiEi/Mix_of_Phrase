using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LampPositionController : MonoBehaviour
{
    private Renderer wall;
    [SerializeField] Pos pos;
    private enum Pos
    {
        UpRight , UpLeft, DownRight, DownLeft 
    };

    void Start()
    {
        foreach (Transform bro in this.transform.parent.transform)
        {
            if (bro.name == "Wall")
                wall = bro.GetComponent<Renderer>();
        }
    }

    void Update()
    {
        if (wall != null)
        {
            float offsetX = 0;
            float offsetY = 0;
            int multX = 1;
            int multY = 1;

            if (pos == Pos.UpRight)
            {
                offsetX = wall.bounds.max.x;
                offsetY = wall.bounds.max.y;

                multX = 1;
                multY = 1;
            }
            if (pos == Pos.UpLeft)
            {
                offsetX = wall.bounds.min.x;
                offsetY = wall.bounds.max.y;

                multX = -1;
                multY = 1;
            }
            if (pos == Pos.DownRight)
            {
                offsetX = wall.bounds.max.x;
                offsetY = wall.bounds.min.y;

                multX = 1;
                multY = -1;
            }
            if (pos == Pos.DownLeft)
            {
                offsetX = wall.bounds.min.x;
                offsetY = wall.bounds.min.y;

                multX = -1;
                multY = -1;
            }

            transform.position = new Vector3(offsetX - 0.2f * multX, offsetY - 0.2f * multY, 0);
        }
    }
}