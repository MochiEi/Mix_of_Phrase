using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBlockController : MonoBehaviour
{
    private enum BlockColor { Red, Blue };
    [SerializeField] BlockColor blockColor;

    [SerializeField] Transform colorBlockManager;
    private ActiveColorController colorController;

    private string thisColor;
    private Animator anim;

    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;

    void Start()
    {
        colorController = colorBlockManager.GetComponent<ActiveColorController>();

        thisColor = blockColor.ToString();
        anim = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();

        UpdateColliderSize();
    }

    void Update()
    {
        if (thisColor == colorController.ActiveColor())
            anim.SetBool("isActive", true);
        else
            anim.SetBool("isActive", false);
    }

    void UpdateColliderSize()
    {
        Vector2 spriteSize = spriteRenderer.size;
        Vector2[] newPoints = new Vector2[8];

        newPoints[0] = new Vector2(spriteSize.x / 2, spriteSize.y / 2 - 0.01f);
        newPoints[1] = new Vector2(spriteSize.x / 2, -(spriteSize.y / 2 - 0.01f));
        newPoints[2] = new Vector2(spriteSize.x / 2 - 0.01f, -(spriteSize.y / 2));
        newPoints[3] = new Vector2(-(spriteSize.x / 2 - 0.01f), -(spriteSize.y / 2));
        newPoints[4] = new Vector2(-(spriteSize.x / 2), -(spriteSize.y / 2 - 0.01f));
        newPoints[5] = new Vector2(-(spriteSize.x / 2), spriteSize.y / 2 - 0.01f);
        newPoints[6] = new Vector2(-(spriteSize.x / 2 - 0.01f), spriteSize.y / 2);
        newPoints[7] = new Vector2(spriteSize.x / 2 - 0.01f, spriteSize.y / 2);

        polygonCollider.points = newPoints;
    }
}