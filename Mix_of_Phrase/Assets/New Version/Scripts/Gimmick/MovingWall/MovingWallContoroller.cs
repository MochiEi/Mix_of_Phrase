using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Playables;

public class MovingWallContoroller : MonoBehaviour
{
    private Transform wall;
    private Renderer renderer;
    private SpriteRenderer lamp;
    private ButtonController trigger;

    private Collider2D stopCollider;
    private List<Collider2D> result = new List<Collider2D>();

    private bool isStop;

    [Header("ÉgÉäÉKÅ[")]
    [SerializeField] GameObject setTrigger;


    [Header("êLèkìÆçÏ")]
    [SerializeField] Stretch stretch;
    private enum Stretch
    {
        Expand, Shrink
    };

    [Header("êLèkï˚å¸")]
    [SerializeField] Direction direction;
    private enum Direction
    {
        Up, Down, Right, Left
    }

    [Header("ìÆçÏå„ÇÃÉTÉCÉY")]
    [SerializeField] float activeSize;

    [Header("ìÆçÏë¨ìx")]
    [SerializeField] float speed;

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Wall")
            {
                wall = child;
                renderer = wall.GetComponent<Renderer>();
            }
            if (child.name == "Lamp")
            {
                lamp = child.gameObject.GetComponent<SpriteRenderer>();
            }
            if (child.name == "StopCollider")
            {
                stopCollider = child.gameObject.GetComponent<Collider2D>();
            }
        }
    }

    void Update()
    {
        trigger = setTrigger.GetComponent<ButtonController>();

        Overlap();

        if (trigger.IsActive())
        {
            isStop = false;
            speed = Mathf.Abs(speed);
        }
        else
        {
            speed = -Mathf.Abs(speed);
        }

        if (isStop)
        {
            lamp.color = Color.red;
        }
        else
        {
            lamp.color = Color.green;

            if (stretch == Stretch.Expand)
                StretchExpand();
            if (stretch == Stretch.Shrink)
                StretchShrink();
        }

    }

    private void StretchExpand()
    {
        float scaleX = wall.localScale.x;
        float scaleY = wall.localScale.y;

        if (direction == Direction.Up)
        {
            float currentBottomY = renderer.bounds.min.y;

            scaleY += speed * Time.deltaTime;
            scaleY = Mathf.Min(scaleY, activeSize);
            wall.localScale = new Vector3(1, scaleY, 1);

            float newBottomY = renderer.bounds.min.y;

            float offsetY = currentBottomY - newBottomY;

            wall.position += new Vector3(0, offsetY, 0);
        }

        if (direction == Direction.Down)
        {
            float currentBottomY = renderer.bounds.max.y;

            scaleY += speed * Time.deltaTime;
            scaleY = Mathf.Min(scaleY, activeSize);
            wall.localScale = new Vector3(1, scaleY, 1);

            float newBottomY = renderer.bounds.max.y;

            float offsetY = currentBottomY - newBottomY;

            wall.position += new Vector3(0, offsetY, 0);
        }

        if (direction == Direction.Right)
        {
            float currentBottomX = renderer.bounds.min.x;

            scaleX += speed * Time.deltaTime;
            scaleX = Mathf.Min(scaleX, activeSize);
            wall.localScale = new Vector3(scaleX, 1, 1);

            float newBottomX = renderer.bounds.min.x;

            float offsetX = currentBottomX - newBottomX;

            wall.position += new Vector3(offsetX, 0, 0);
        }

        if (direction == Direction.Left)
        {
            float currentBottomX = renderer.bounds.max.x;

            scaleX += speed * Time.deltaTime;
            scaleX = Mathf.Min(scaleX, activeSize);
            wall.localScale = new Vector3(scaleX, 1, 1);

            float newBottomX = renderer.bounds.max.x;

            float offsetX = currentBottomX - newBottomX;

            wall.position += new Vector3(offsetX, 0, 0);
        }
    }

    private void StretchShrink()
    {
        float scaleX = wall.localScale.x;
        float scaleY = wall.localScale.y;

        if (direction == Direction.Up)
        {
            float currentBottomY = renderer.bounds.max.y;

            scaleY -= speed * Time.deltaTime;
            scaleY = Mathf.Max(scaleY, activeSize);
            wall.localScale = new Vector3(1, scaleY, 1);

            float newBottomY = renderer.bounds.max.y;

            float offsetY = currentBottomY - newBottomY;

            wall.position += new Vector3(0, offsetY, 0);
        }

        if (direction == Direction.Down)
        {
            float currentBottomY = renderer.bounds.min.y;

            scaleY -= speed * Time.deltaTime;
            scaleY = Mathf.Max(scaleY, activeSize);
            wall.localScale = new Vector3(1, scaleY, 1);

            float newBottomY = renderer.bounds.min.y;

            float offsetY = currentBottomY - newBottomY;

            wall.position += new Vector3(0, offsetY, 0);
        }

        if (direction == Direction.Right)
        {
            float currentBottomX = renderer.bounds.max.x;

            scaleX -= speed * Time.deltaTime;
            scaleX = Mathf.Max(scaleX, activeSize);
            wall.localScale = new Vector3(scaleX, 1, 1);

            float newBottomX = renderer.bounds.max.x;

            float offsetX = currentBottomX - newBottomX;

            wall.position += new Vector3(offsetX, 0, 0);
        }

        if (direction == Direction.Left)
        {
            float currentBottomX = renderer.bounds.min.x;

            scaleX -= speed * Time.deltaTime;
            scaleX = Mathf.Max(scaleX, activeSize);
            wall.localScale = new Vector3(scaleX, 1, 1);

            float newBottomX = renderer.bounds.min.x;

            float offsetX = currentBottomX - newBottomX;

            wall.position += new Vector3(offsetX, 0, 0);
        }
    }

    private void Overlap()
    {
        int count = stopCollider.OverlapCollider(new ContactFilter2D(), result);

        if (count > 0)
        {
            foreach (Collider2D c in result)
            {                
                if (!c.gameObject.CompareTag("MovingWall"))
                {
                    isStop = true;
                }
            }
        }
    }
}