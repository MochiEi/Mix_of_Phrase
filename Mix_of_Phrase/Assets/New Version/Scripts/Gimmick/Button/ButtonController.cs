using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour, ActiveCheck
{
    private Collider2D activeHitBox;
    private List<Collider2D> result = new List<Collider2D>();

    private bool isActive;
    private Animator anim;

    [SerializeField] string[] unHitTags;

    private void Start()
    {
        foreach (Transform child in transform)
            activeHitBox = child.GetComponent<Collider2D>();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Overlap();

        if (isActive)
            anim.SetBool("isPush", true);
        else
            anim.SetBool("isPush", false);
    }

    private void Overlap()
    {
        int count = activeHitBox.OverlapCollider(new ContactFilter2D(), result);

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