using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInsertGimmickController : MonoBehaviour, ActiveCheck
{
    private SpriteRenderer lamp;

    private Collider2D hitBox;
    private List<Collider2D> result = new List<Collider2D>();

    private bool isActive;

    [SerializeField] string[] unHitTags;
    private void Start()
    {
        hitBox = GetComponent<Collider2D>();
        lamp = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Overlap();

        if (isActive)
            lamp.color = Color.green;
        else
            lamp.color = Color.red;
    }

    private void Overlap()
    {
        int count = hitBox.OverlapCollider(new ContactFilter2D(), result);

        if (count > 0)
        {
            isActive = false;

            foreach (Collider2D c in result)
            {
                bool isBlacklisted = false;

                foreach (string unHitTag in unHitTags)
                {
                    if (c.tag == unHitTag)
                    {
                        isBlacklisted = true;
                        break;
                    }
                }

                if (!isBlacklisted)
                {
                    isActive = true;
                    break;
                }
            }
        }
        else
        {
            isActive = false;
        }
    }

    public bool IsActive()
    {
        return isActive;
    }
}
