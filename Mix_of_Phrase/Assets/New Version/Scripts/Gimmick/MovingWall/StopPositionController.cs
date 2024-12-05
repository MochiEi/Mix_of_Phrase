using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPositionController : MonoBehaviour
{
    private Renderer wall;
    private BoxCollider2D wallCollider;
    [SerializeField] Direction direction;
    private enum Direction
    {
        Up, Down, Right, Left
    }

    void Start()
    {
        foreach (Transform bro in this.transform.parent.transform)
        {
            if (bro.name == "Wall")
                wall = bro.GetComponent<Renderer>();
        }

        wallCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (wall != null)
        {
            float offsetX = 0;
            float offsetY = 0;
            float addX = 0;
            float addY = 0;

            if (direction == Direction.Up)
            {
                offsetX = wall.bounds.center.x;
                offsetY = wall.bounds.max.y;
                wallCollider.size = new Vector2(0.9f, 0.2f);
                addX = 0; addY = -0.1f;
            }
            if (direction == Direction.Down)
            {
                offsetX = wall.bounds.center.x;
                offsetY = wall.bounds.min.y;
                wallCollider.size = new Vector2(0.9f, 0.2f);
                addX = 0; addY = 0.1f;
            }
            if (direction == Direction.Right)
            {
                offsetX = wall.bounds.max.x;
                offsetY = wall.bounds.center.y;
                wallCollider.size = new Vector2(0.2f, 0.9f);
                addX = -0.1f; addY = 0;
            }
            if (direction == Direction.Left)
            {
                offsetX = wall.bounds.min.x;
                offsetY = wall.bounds.center.y;
                wallCollider.size = new Vector2(0.2f, 0.9f);
                addX = 0.1f; addY = 0;
            }

            transform.position = new Vector3(offsetX + addX, offsetY + addY, 0);
        }
    }
}