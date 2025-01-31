using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveColorController : MonoBehaviour
{
    [SerializeField] Transform pox;
    private Collider2D poxColl;
    [SerializeField] List<Collider2D> hitColl = new List<Collider2D>();
    private ContactFilter2D filter = new ContactFilter2D();
    private bool ishit;

    private enum SelectColor { Red, Blue };
    [SerializeField] SelectColor selectColor;

    void Start()
    {
        poxColl = pox.GetComponent<Collider2D>();

        filter.useTriggers = true;
        filter.useLayerMask = true;
        filter.SetLayerMask(Physics2D.GetLayerCollisionMask(poxColl.gameObject.layer));
    }

    void Update()
    {
        ishit = false;
        hitColl.RemoveAll(coll => coll = null);
        int count = poxColl.OverlapCollider(filter, hitColl);

        foreach (var hitObj in hitColl)
        {
            if (hitObj == null) continue;

            if (hitObj.isTrigger)
            {
                ishit = true;
                break;
            }
        }
    }

    public void IsActiveRed()
    {
        if (ishit) return;
        selectColor = SelectColor.Red;
    }
    public void IsActiveBlue()
    {
        if (ishit) return;
        selectColor = SelectColor.Blue;
    }

    public string ActiveColor()
    {
        return selectColor.ToString();
    }
}